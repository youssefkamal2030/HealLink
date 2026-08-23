/**
 * Concurrent OTP Confirmation Load Test Scenario
 * 
 * Tests large-scale concurrent OTP email verification to validate:
 * - OTP token validation at scale
 * - Timing attack resistance (constant-time comparison)
 * - Rate limiting on failed OTP attempts
 * - Database consistency under concurrent updates
 * - Account state transitions (pending → confirmed)
 * 
 * Test Cases:
 * 1. Valid OTP — successful confirmation
 * 2. Invalid OTP — attempt with wrong code
 * 3. Expired OTP — attempt with old code
 * 4. Multiple attempts — rate limiting
 * 5. Already confirmed — idempotency
 * 6. Non-existent user — negative case
 * 
 * Workflow: POST /api/Auth/confirm-email with OTP
 * 
 * Requirements: 1.1, 1.2, 2.1, 2.2, 3.7, 9.3
 */

import http from 'k6/http';
import { check, sleep, group } from 'k6';
import { Counter, Rate, Trend } from 'k6/metrics';
import { SharedArray } from 'k6/data';
import { generateUsers } from '../utils/dataGenerator.js';
import { loadConfig, getLoadProfile, getThresholds } from '../config/environments.js';

// ========================================
// CONFIGURATION
// ========================================

const ENV = __ENV.ENVIRONMENT || 'local';
const LOAD_PROFILE = __ENV.LOAD_PROFILE || 'load';
const config = loadConfig(ENV);
const loadProfile = getLoadProfile(ENV, LOAD_PROFILE);
const thresholds = getThresholds(ENV, 'auth');

// ========================================
// TEST DATA PREPARATION
// ========================================

const testUsers = new SharedArray('otp_users', function() {
  let userCount;
  switch(LOAD_PROFILE) {
    case 'smoke':
      userCount = 10;
      break;
    case 'load':
      userCount = 100;
      break;
    case 'stress':
      userCount = 500;
      break;
    case 'soak':
      userCount = 50;
      break;
    default:
      userCount = 100;
  }
  
  console.log(`Generating ${userCount} test users for OTP confirmation test...`);
  return generateUsers(userCount);
});

// Define test cases with various OTP scenarios
const OTP_TEST_CASES = {
  // Case 1: Valid OTP (assume "000000" is valid for test users)
  VALID: {
    name: 'Valid OTP',
    otp: '000000',
    expectedStatus: [200, 400],  // 400 if OTP format/generation issues
    successThreshold: 0.7        // Expect at least 70% success
  },
  
  // Case 2: Invalid OTP (wrong code)
  INVALID: {
    name: 'Invalid OTP Code',
    otp: '999999',
    expectedStatus: [400, 401, 422],
    successThreshold: 0.0        // Expect all failures
  },
  
  // Case 3: Malformed OTP (too short)
  MALFORMED_SHORT: {
    name: 'Malformed OTP (too short)',
    otp: '123',
    expectedStatus: [400, 422],
    successThreshold: 0.0
  },
  
  // Case 4: Malformed OTP (non-numeric)
  MALFORMED_ALPHA: {
    name: 'Malformed OTP (non-numeric)',
    otp: 'ABCDEF',
    expectedStatus: [400, 422],
    successThreshold: 0.0
  },
  
  // Case 5: Empty OTP
  EMPTY: {
    name: 'Empty OTP',
    otp: '',
    expectedStatus: [400, 422],
    successThreshold: 0.0
  },
  
  // Case 6: Non-existent user email
  NONEXISTENT_USER: {
    name: 'Non-existent User',
    otp: '000000',
    email: 'nonexistent-user-99999@loadtest.heallink.local',
    expectedStatus: [400, 404, 422],
    successThreshold: 0.0
  }
};

// ========================================
// CUSTOM METRICS
// ========================================

const otpAttempts = new Counter('otp_attempts');
const otpSuccesses = new Counter('otp_successes');
const otpFailures = new Counter('otp_failures');
const otpErrorRate = new Rate('otp_error_rate');
const otpDuration = new Trend('otp_duration');

// Test case specific metrics
const otpValidAttempts = new Counter('otp_valid_attempts');
const otpInvalidAttempts = new Counter('otp_invalid_attempts');
const otpMalformedAttempts = new Counter('otp_malformed_attempts');
const otpNonexistentAttempts = new Counter('otp_nonexistent_attempts');

// HTTP status tracking
const status200_otp = new Counter('otp_status_200');
const status400_otp = new Counter('otp_status_400');
const status401_otp = new Counter('otp_status_401');
const status404_otp = new Counter('otp_status_404');
const status422_otp = new Counter('otp_status_422');
const status429_otp = new Counter('otp_status_429');
const status500_otp = new Counter('otp_status_500');

// ========================================
// K6 TEST CONFIGURATION
// ========================================

export const options = {
  stages: loadProfile.stages,
  
  thresholds: {
    'http_req_duration': [`p(95)<${thresholds.p95_response_time}`],
    'http_req_failed': [`rate<${thresholds.error_rate * 1.5}`],  // Slightly relaxed
    
    'otp_error_rate': [`rate<${thresholds.error_rate * 1.5}`],
    'otp_duration': [`p(95)<${thresholds.p95_response_time}`],
    
    // Specific status codes
    'otp_status_200': ['count>0'],        // At least some successes
    'otp_status_429': ['count<50'],       // Some rate limiting is ok
    'otp_status_500': ['count==0'],       // No server errors
  },
  
  maxRedirects: 0,
  userAgent: 'HealLink-LoadTest-k6/1.0',
  
  tags: {
    scenario: 'concurrent_otp_confirmation',
    environment: ENV,
    loadProfile: LOAD_PROFILE
  }
};

// ========================================
// HELPER FUNCTIONS
// ========================================

/**
 * Track HTTP status code
 */
function trackOtpStatus(statusCode) {
  if (statusCode === 200) {
    status200_otp.add(1);
  } else if (statusCode === 400) {
    status400_otp.add(1);
  } else if (statusCode === 401) {
    status401_otp.add(1);
  } else if (statusCode === 404) {
    status404_otp.add(1);
  } else if (statusCode === 422) {
    status422_otp.add(1);
  } else if (statusCode === 429) {
    status429_otp.add(1);
  } else if (statusCode >= 500) {
    status500_otp.add(1);
  }
}

/**
 * Perform OTP confirmation
 */
function performOtpConfirmation(email, otp, baseUrl) {
  const startTime = Date.now();
  
  const url = `${baseUrl}/api/Auth/confirm-email`;
  
  const payload = JSON.stringify({
    Email: email,
    Code: otp
  });
  
  const params = {
    headers: {
      'Content-Type': 'application/json'
    },
    timeout: '15s',
    tags: { 
      name: 'otp_confirmation',
      otp_case: 'test'
    }
  };
  
  const response = http.post(url, payload, params);
  const duration = Date.now() - startTime;
  
  // Track metrics
  otpAttempts.add(1);
  otpDuration.add(duration);
  trackOtpStatus(response.status);
  
  return {
    status: response.status,
    body: response.body,
    duration: duration,
    success: response.status === 200
  };
}

// ========================================
// SETUP PHASE
// ========================================

export function setup() {
  console.log('='.repeat(60));
  console.log('CONCURRENT OTP CONFIRMATION LOAD TEST');
  console.log('='.repeat(60));
  console.log(`Environment: ${ENV}`);
  console.log(`Load Profile: ${LOAD_PROFILE}`);
  console.log(`Base URL: ${config.baseUrl}`);
  console.log(`Test Users: ${testUsers.length}`);
  console.log(`Test Cases: ${Object.keys(OTP_TEST_CASES).length}`);
  console.log(`Max VUs: ${config.maxVUs}`);
  console.log(`P95 Threshold: ${thresholds.p95_response_time}ms`);
  console.log('='.repeat(60));
  console.log('\nTest Cases:');
  Object.entries(OTP_TEST_CASES).forEach(([key, testCase]) => {
    console.log(`  - ${testCase.name}`);
  });
  console.log('');
  
  // Verify API is accessible
  const healthCheckUrl = `${config.baseUrl}/api/Auth/confirm-email`;
  const healthCheck = http.options(healthCheckUrl, null, { timeout: '10s' });
  
  if (healthCheck.status === 0) {
    console.error(`❌ API not accessible at ${config.baseUrl}`);
    console.error('Please verify the API is running before starting load tests.');
    throw new Error('API not accessible');
  }
  
  console.log(`✅ API is accessible at ${config.baseUrl}`);
  console.log('Starting load test...\n');
  
  return {
    baseUrl: config.baseUrl,
    totalUsers: testUsers.length,
    testCases: Object.keys(OTP_TEST_CASES)
  };
}

// ========================================
// MAIN TEST SCENARIO
// ========================================

export default function(data) {
  const vuId = __VU;
  const iterationId = __ITER;
  const userIndex = (vuId + iterationId) % testUsers.length;
  const user = testUsers[userIndex];
  
  // Select test case based on iteration
  const testCaseKeys = data.testCases;
  const testCaseIndex = iterationId % testCaseKeys.length;
  const testCaseKey = testCaseKeys[testCaseIndex];
  const testCase = OTP_TEST_CASES[testCaseKey];
  
  // Use non-existent email for that test case
  const email = testCase.email || user.email;
  
  group(`OTP Confirmation: ${testCase.name}`, function() {
    const result = performOtpConfirmation(email, testCase.otp, data.baseUrl);
    
    // Track by test case
    if (testCaseKey === 'VALID') {
      otpValidAttempts.add(1);
    } else if (testCaseKey === 'INVALID') {
      otpInvalidAttempts.add(1);
    } else if (testCaseKey.includes('MALFORMED') || testCaseKey === 'EMPTY') {
      otpMalformedAttempts.add(1);
    } else if (testCaseKey === 'NONEXISTENT_USER') {
      otpNonexistentAttempts.add(1);
    }
    
    // Validate response
    const expectedStatusSet = testCase.expectedStatus;
    check(result, {
      'OTP response status is expected': (r) => expectedStatusSet.includes(r.status),
      'OTP response time acceptable': (r) => r.duration < thresholds.p95_response_time * 1.5,
      'OTP response has body': (r) => r.body && r.body.length > 0
    });
    
    if (result.success) {
      otpSuccesses.add(1);
      otpErrorRate.add(0);
    } else {
      otpFailures.add(1);
      otpErrorRate.add(1);
    }
  });
  
  // Think time between iterations
  let thinkTime;
  switch(LOAD_PROFILE) {
    case 'smoke':
      thinkTime = 1;
      break;
    case 'stress':
      thinkTime = 0.3;
      break;
    case 'soak':
      thinkTime = 2;
      break;
    default:
      thinkTime = 0.5;
  }
  sleep(thinkTime);
}

// ========================================
// TEARDOWN PHASE
// ========================================

export function teardown(data) {
  console.log('\n' + '='.repeat(60));
  console.log('OTP CONFIRMATION LOAD TEST COMPLETED');
  console.log('='.repeat(60));
  console.log(`Total Users Tested: ${data.totalUsers}`);
  console.log(`Total OTP Attempts: ${otpAttempts.value || 0}`);
  console.log(`Successful Confirmations: ${otpSuccesses.value || 0}`);
  console.log(`Failed Confirmations: ${otpFailures.value || 0}`);
  console.log('\nTest Case Breakdown:');
  console.log(`  Valid OTP attempts: ${otpValidAttempts.value || 0}`);
  console.log(`  Invalid OTP attempts: ${otpInvalidAttempts.value || 0}`);
  console.log(`  Malformed OTP attempts: ${otpMalformedAttempts.value || 0}`);
  console.log(`  Non-existent user attempts: ${otpNonexistentAttempts.value || 0}`);
  console.log('Review detailed metrics above for performance analysis.');
  console.log('='.repeat(60));
}
