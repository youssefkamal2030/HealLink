/**
 * Stress Test: User Registration (Signup) Endpoint
 * 
 * Tests large-scale concurrent user registration to validate:
 * - Database write throughput under concurrent load
 * - Email service capacity (OTP generation and sending)
 * - Request validation and duplicate detection
 * - Connection pooling for write operations
 * - Rate limiting and throttling behavior
 * 
 * This scenario simulates realistic signup storms during:
 * - Product launch or marketing campaign
 * - High-traffic periods (new year resolutions)
 * - Social media virality
 * 
 * Workflow: POST /api/Auth/register → OTP sent to email
 * 
 * Requirements: 1.1, 2.1, 3.1, 3.2, 3.3
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
const LOAD_PROFILE = __ENV.LOAD_PROFILE || 'stress';
const config = loadConfig(ENV);
const loadProfile = getLoadProfile(ENV, LOAD_PROFILE);
const thresholds = getThresholds(ENV, 'auth');

// ========================================
// TEST DATA PREPARATION
// ========================================

const testUsers = new SharedArray('signup_users', function() {
  let userCount;
  switch(LOAD_PROFILE) {
    case 'smoke':
      userCount = 20;
      break;
    case 'load':
      userCount = 500;
      break;
    case 'stress':
      userCount = 1000;
      break;
    case 'soak':
      userCount = 100;
      break;
    default:
      userCount = 500;
  }
  
  console.log(`Generating ${userCount} test users for ${LOAD_PROFILE} profile...`);
  return generateUsers(userCount);
});

// ========================================
// CUSTOM METRICS
// ========================================

const signupAttempts = new Counter('signup_attempts');
const signupSuccesses = new Counter('signup_successes');
const signupFailures = new Counter('signup_failures');
const signupErrorRate = new Rate('signup_error_rate');
const signupDuration = new Trend('signup_duration');
const otpSentCount = new Counter('otp_sent_count');

// HTTP status tracking
const status200_signup = new Counter('signup_status_200');
const status201_signup = new Counter('signup_status_201');
const status400_signup = new Counter('signup_status_400');
const status422_signup = new Counter('signup_status_422');
const status429_signup = new Counter('signup_status_429');
const status500_signup = new Counter('signup_status_500');

// ========================================
// K6 TEST CONFIGURATION
// ========================================

export const options = {
  stages: loadProfile.stages,
  
  thresholds: {
    'http_req_duration': [`p(95)<${thresholds.p95_response_time * 1.5}`],  // More relaxed for stress
    'http_req_failed': [`rate<${thresholds.error_rate * 2}`],             // Allow double error rate
    
    'signup_error_rate': [`rate<${thresholds.error_rate * 2}`],
    'signup_duration': [`p(95)<${thresholds.p95_response_time * 1.5}`],
    
    // Specific status codes
    'signup_status_400': ['count<50'],   // Max 50 bad requests (validation failures)
    'signup_status_429': ['count<20'],   // Max 20 rate-limited (some expected under stress)
    'signup_status_500': ['count==0'],   // No server errors
  },
  
  maxRedirects: 0,
  userAgent: 'HealLink-LoadTest-k6/1.0',
  
  tags: {
    scenario: 'stress_signup',
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
function trackSignupStatus(statusCode) {
  if (statusCode === 200) {
    status200_signup.add(1);
  } else if (statusCode === 201) {
    status201_signup.add(1);
  } else if (statusCode === 400) {
    status400_signup.add(1);
  } else if (statusCode === 422) {
    status422_signup.add(1);
  } else if (statusCode === 429) {
    status429_signup.add(1);
  } else if (statusCode >= 500) {
    status500_signup.add(1);
  }
}

/**
 * Perform signup with form-encoded data
 */
function performSignup(user, baseUrl) {
  const startTime = Date.now();
  
  const url = `${baseUrl}/api/Auth/register`;
  
  // Build form data for multipart form submission
  const formData = {
    username: user.username,
    Password: user.password,
    Email: user.email,
    Role: user.role,
    PracticeLisenceNumber: '',
    Specilization: ''
  };
  
  // Encode form data
  const encodedData = Object.keys(formData)
    .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(formData[key])}`)
    .join('&');
  
  const params = {
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded'
    },
    timeout: '30s',
    tags: { 
      name: 'signup_register',
      user_role: user.role
    }
  };
  
  const response = http.post(url, encodedData, params);
  const duration = Date.now() - startTime;
  
  // Track metrics
  signupAttempts.add(1);
  signupDuration.add(duration);
  trackSignupStatus(response.status);
  
  // Validate response
  check(response, {
    'signup status is 2xx': (r) => r.status >= 200 && r.status < 300,
    'signup response time acceptable': (r) => r.timings.duration < thresholds.p95_response_time * 1.5,
    'signup response has body': (r) => r.body && r.body.length > 0,
    'signup response is valid JSON': (r) => {
      try {
        JSON.parse(r.body);
        return true;
      } catch(e) {
        return false;
      }
    }
  });
  
  if (response.status === 200 || response.status === 201) {
    signupSuccesses.add(1);
    signupErrorRate.add(0);
    otpSentCount.add(1);  // OTP sent when signup successful
    
    try {
      const responseBody = JSON.parse(response.body);
      return {
        success: true,
        userId: responseBody.userId,
        email: user.email,
        duration: duration
      };
    } catch(e) {
      return {
        success: true,
        email: user.email,
        duration: duration
      };
    }
  } else {
    signupFailures.add(1);
    signupErrorRate.add(1);
    
    // Log specific error details
    if (response.status === 400) {
      // Validation error - expected for some edge cases
      console.warn(`Signup validation failed (400) for ${user.email}`);
    } else if (response.status === 422) {
      // Unprocessable entity - duplicate or constraint violation
      console.warn(`Signup conflict (422) for ${user.email} - may already exist`);
    } else if (response.status === 429) {
      console.warn(`Signup rate limited (429) for ${user.email}`);
    } else if (response.status >= 500) {
      console.error(`Server error (${response.status}) for ${user.email}: ${response.body.substring(0, 200)}`);
    }
    
    return {
      success: false,
      error: `HTTP ${response.status}`,
      email: user.email,
      duration: duration
    };
  }
}

// ========================================
// SETUP PHASE
// ========================================

export function setup() {
  console.log('='.repeat(60));
  console.log('STRESS TEST: USER REGISTRATION (SIGNUP)');
  console.log('='.repeat(60));
  console.log(`Environment: ${ENV}`);
  console.log(`Load Profile: ${LOAD_PROFILE}`);
  console.log(`Base URL: ${config.baseUrl}`);
  console.log(`Test Users: ${testUsers.length}`);
  console.log(`Max VUs: ${config.maxVUs}`);
  console.log(`P95 Threshold: ${(thresholds.p95_response_time * 1.5).toFixed(0)}ms (relaxed for stress)`);
  console.log(`Error Rate Threshold: ${(thresholds.error_rate * 200).toFixed(1)}%`);
  console.log('='.repeat(60));
  
  // Verify API is accessible
  const healthCheckUrl = `${config.baseUrl}/api/Auth/register`;
  const healthCheck = http.options(healthCheckUrl, null, { timeout: '10s' });
  
  if (healthCheck.status === 0) {
    console.error(`❌ API not accessible at ${config.baseUrl}`);
    console.error('Please verify the API is running before starting load tests.');
    throw new Error('API not accessible');
  }
  
  console.log(`✅ API is accessible at ${config.baseUrl}`);
  console.log('Starting stress test...\n');
  
  return {
    baseUrl: config.baseUrl,
    totalUsers: testUsers.length
  };
}

// ========================================
// MAIN TEST SCENARIO
// ========================================

export default function(data) {
  // Get a unique user for this VU iteration
  const vuId = __VU;
  const iterationId = __ITER;
  const userIndex = (vuId + iterationId) % testUsers.length;
  const user = testUsers[userIndex];
  
  // Group: Signup
  group('User Registration', function() {
    const result = performSignup(user, data.baseUrl);
    
    if (result.success) {
      group('Post-Signup Validation', function() {
        check(result, {
          'signup completed': (r) => r.success === true,
          'user email matches': (r) => r.email === user.email,
          'response time recorded': (r) => r.duration !== undefined
        });
      });
    }
  });
  
  // Think time between iterations
  let thinkTime;
  switch(LOAD_PROFILE) {
    case 'smoke':
      thinkTime = 2;
      break;
    case 'stress':
      thinkTime = 0.2;  // Very aggressive
      break;
    case 'soak':
      thinkTime = 3;
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
  console.log('STRESS TEST COMPLETED');
  console.log('='.repeat(60));
  console.log(`Total Users Tested: ${data.totalUsers}`);
  console.log(`OTP Sent Count: ${otpSentCount.value || 0}`);
  console.log('Review detailed metrics above for performance analysis.');
  console.log('='.repeat(60));
}
