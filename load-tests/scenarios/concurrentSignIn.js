/**
 * Concurrent Sign-In Load Test Scenario
 * 
 * Tests large-scale concurrent user authentication to validate:
 * - Database connection pooling under concurrent load
 * - JWT token generation performance at scale
 * - API authentication throughput limits
 * - Password hashing performance (BCrypt)
 * - Thread safety of authentication pipeline
 * 
 * This scenario simulates realistic user sign-in storms during:
 * - Peak usage hours (morning login rush)
 * - System recovery after downtime
 * - Marketing campaigns driving traffic spikes
 * 
 * Requirements: 1.1, 1.2, 1.3, 2.1, 9.1, 9.2
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

// Load environment configuration
const ENV = __ENV.ENVIRONMENT || 'local';
const LOAD_PROFILE = __ENV.LOAD_PROFILE || 'load';
const config = loadConfig(ENV);
const loadProfile = getLoadProfile(ENV, LOAD_PROFILE);
const thresholds = getThresholds(ENV, 'auth');

// ========================================
// TEST DATA PREPARATION
// ========================================

// Generate test users - shared across all VUs
const testUsers = new SharedArray('concurrent_signin_users', function() {
  // Generate different user counts based on load profile
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
  
  console.log(`Generating ${userCount} test users for ${LOAD_PROFILE} profile...`);
  return generateUsers(userCount);
});

// ========================================
// CUSTOM METRICS
// ========================================

const loginAttempts = new Counter('concurrent_login_attempts');
const loginSuccesses = new Counter('concurrent_login_successes');
const loginFailures = new Counter('concurrent_login_failures');
const loginErrorRate = new Rate('concurrent_login_error_rate');
const loginDuration = new Trend('concurrent_login_duration');
const tokenValidations = new Counter('token_validations');
const tokenValidationFailures = new Counter('token_validation_failures');

// HTTP status tracking
const status200 = new Counter('http_status_200');
const status401 = new Counter('http_status_401');
const status429 = new Counter('http_status_429');
const status500 = new Counter('http_status_500');
const statusOther = new Counter('http_status_other');

// ========================================
// K6 TEST CONFIGURATION
// ========================================

export const options = {
  // Use environment-specific load profile
  stages: loadProfile.stages,
  
  // Performance thresholds
  thresholds: {
    // Overall HTTP metrics
    'http_req_duration': [`p(95)<${thresholds.p95_response_time}`],
    'http_req_failed': [`rate<${thresholds.error_rate}`],
    
    // Custom metrics thresholds
    'concurrent_login_error_rate': [`rate<${thresholds.error_rate}`],
    'concurrent_login_duration': [`p(95)<${thresholds.p95_response_time}`],
    
    // Specific HTTP status thresholds
    'http_status_401': ['count<10'],  // Max 10 auth failures expected
    'http_status_429': ['count==0'],  // No rate limiting expected
    'http_status_500': ['count==0'],  // No server errors expected
    
    // Token validation success rate
    'token_validation_failures': ['count==0']
  },
  
  // Test duration and resource limits
  maxRedirects: 0,
  userAgent: 'HealLink-LoadTest-k6/1.0',
  
  // Tags for metric filtering
  tags: {
    scenario: 'concurrent_signin',
    environment: ENV,
    loadProfile: LOAD_PROFILE
  }
};

// ========================================
// HELPER FUNCTIONS
// ========================================

/**
 * Validate JWT token format
 */
function isValidJWT(token) {
  if (!token || typeof token !== 'string') {
    return false;
  }
  const parts = token.split('.');
  return parts.length === 3 && parts.every(part => part.length > 0);
}

/**
 * Track HTTP status code
 */
function trackHttpStatus(statusCode) {
  if (statusCode === 200) {
    status200.add(1);
  } else if (statusCode === 401) {
    status401.add(1);
  } else if (statusCode === 429) {
    status429.add(1);
  } else if (statusCode >= 500) {
    status500.add(1);
  } else {
    statusOther.add(1);
  }
}

/**
 * Perform sign-in with detailed tracking
 */
function performSignIn(user, baseUrl) {
  const startTime = Date.now();
  
  const url = `${baseUrl}/api/Auth/login`;
  const payload = JSON.stringify({
    Email: user.email,
    Password: user.password
  });
  
  const params = {
    headers: {
      'Content-Type': 'application/json'
    },
    timeout: '30s',
    tags: { 
      name: 'concurrent_signin_login',
      user_role: user.role
    }
  };
  
  const response = http.post(url, payload, params);
  const duration = Date.now() - startTime;
  
  // Track metrics
  loginAttempts.add(1);
  loginDuration.add(duration);
  trackHttpStatus(response.status);
  
  // Validate response
  check(response, {
    'login status is 200': (r) => r.status === 200,
    'response time under threshold': (r) => r.timings.duration < thresholds.p95_response_time,
    'login response has body': (r) => r.body && r.body.length > 0,
    'login response is valid JSON': (r) => {
      try {
        JSON.parse(r.body);
        return true;
      } catch(e) {
        return false;
      }
    }
  });
  
  if (response.status === 200) {
    loginSuccesses.add(1);
    loginErrorRate.add(0);
    
    // Parse response and validate token
    let responseBody;
    try {
      responseBody = JSON.parse(response.body);
    } catch(e) {
      console.error(`Failed to parse response: ${e.message}`);
      loginErrorRate.add(1);
      return { success: false, error: 'Invalid JSON response' };
    }
    
    // Validate token presence
    if (!responseBody.token) {
      console.error('Response missing token field');
      tokenValidationFailures.add(1);
      return { success: false, error: 'Missing token' };
    }
    
    // Validate token format
    tokenValidations.add(1);
    if (!isValidJWT(responseBody.token)) {
      console.error(`Invalid JWT token format received for ${user.email}`);
      tokenValidationFailures.add(1);
      return { success: false, error: 'Invalid token format' };
    }
    
    return {
      success: true,
      token: responseBody.token,
      userId: responseBody.userId,
      username: responseBody.username,
      email: responseBody.Email,
      duration: duration
    };
  } else {
    loginFailures.add(1);
    loginErrorRate.add(1);
    
    // Log specific error details
    if (response.status === 401) {
      console.warn(`Authentication failed (401) for ${user.email}`);
    } else if (response.status === 429) {
      console.warn(`Rate limited (429) for ${user.email}`);
    } else if (response.status >= 500) {
      console.error(`Server error (${response.status}) for ${user.email}: ${response.body}`);
    }
    
    return {
      success: false,
      error: `HTTP ${response.status}`,
      body: response.body
    };
  }
}

// ========================================
// SETUP PHASE
// ========================================

export function setup() {
  console.log('='.repeat(60));
  console.log('CONCURRENT SIGN-IN LOAD TEST');
  console.log('='.repeat(60));
  console.log(`Environment: ${ENV}`);
  console.log(`Load Profile: ${LOAD_PROFILE}`);
  console.log(`Base URL: ${config.baseUrl}`);
  console.log(`Test Users: ${testUsers.length}`);
  console.log(`Max VUs: ${config.maxVUs}`);
  console.log(`P95 Threshold: ${thresholds.p95_response_time}ms`);
  console.log(`Error Rate Threshold: ${(thresholds.error_rate * 100).toFixed(1)}%`);
  console.log('='.repeat(60));
  
  // Verify API is accessible
  const healthCheckUrl = `${config.baseUrl}/api/Auth/login`;
  const healthCheck = http.options(healthCheckUrl, null, { timeout: '10s' });
  
  if (healthCheck.status === 0) {
    console.error(`❌ API not accessible at ${config.baseUrl}`);
    console.error('Please verify the API is running before starting load tests.');
    throw new Error('API not accessible');
  }
  
  console.log(`✅ API is accessible at ${config.baseUrl}`);
  
  // Register test users before load test
  console.log(`\nRegistering ${testUsers.length} test users...`);
  let registrationSuccesses = 0;
  let registrationFailures = 0;
  
  for (let i = 0; i < testUsers.length; i++) {
    const user = testUsers[i];
    const registerUrl = `${config.baseUrl}/api/Auth/register`;
    
    // Build form data for multipart form submission
    const formData = {
      username: user.username,
      Password: user.password,
      Email: user.email,
      Role: user.role,
      PracticeLisenceNumber: '',
      Specilization: ''
      // Note: SyndicateId is IFormFile, we skip it for patient registrations
    };
    
    const params = {
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      timeout: '15s',
      tags: { name: 'setup_register_user' }
    };
    
    // Encode form data
    const encodedData = Object.keys(formData)
      .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(formData[key])}`)
      .join('&');
    
    const registerResponse = http.post(registerUrl, encodedData, params);
    
    if (registerResponse.status === 200 || registerResponse.status === 201) {
      registrationSuccesses++;
    } else if (registerResponse.status === 400) {
      // User may already exist, try to log in to verify
      const loginUrl = `${config.baseUrl}/api/Auth/login`;
      const loginPayload = JSON.stringify({
        Email: user.email,
        Password: user.password
      });
      const loginParams = {
        headers: {
          'Content-Type': 'application/json'
        },
        timeout: '15s',
        tags: { name: 'setup_login_check' }
      };
      const loginResponse = http.post(loginUrl, loginPayload, loginParams);
      if (loginResponse.status === 200) {
        registrationSuccesses++;
      } else {
        registrationFailures++;
        console.error(`Failed to register or login user ${i + 1}: ${registerResponse.status} / ${loginResponse.status}`);
      }
    } else {
      registrationFailures++;
      console.error(`Failed to register user ${i + 1}: ${registerResponse.status} - ${registerResponse.body.substring(0, 200)}`);
    }
  }
  
  console.log(`User Registration Complete: ${registrationSuccesses}/${testUsers.length} successful`);
  if (registrationFailures > 0) {
    console.warn(`⚠️ ${registrationFailures} user(s) failed to register. They may already exist.`);
  }
  
  console.log('Starting load test...\n');
  
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
  // Use modulo to cycle through users if VUs > user count
  const vuId = __VU;
  const iterationId = __ITER;
  const userIndex = (vuId + iterationId) % testUsers.length;
  const user = testUsers[userIndex];
  
  // Group: Concurrent Sign-In
  group('Concurrent Sign-In', function() {
    const result = performSignIn(user, data.baseUrl);
    
    if (result.success) {
      // Success scenario: token received and validated
      group('Post-Login Validation', function() {
        // Additional checks can be added here
        // e.g., verify token claims, check user profile endpoint
        check(result, {
          'token is present': (r) => r.token !== undefined && r.token !== null,
          'userId is valid': (r) => r.userId !== undefined,
          'email matches request': (r) => r.email === user.email
        });
      });
    }
  });
  
  // Think time between iterations (simulate realistic user behavior)
  // More aggressive for stress tests, more relaxed for load tests
  let thinkTime;
  switch(LOAD_PROFILE) {
    case 'smoke':
      thinkTime = 2;
      break;
    case 'stress':
      thinkTime = 0.5; // More aggressive
      break;
    case 'soak':
      thinkTime = 5; // More realistic
      break;
    default:
      thinkTime = 1;
  }
  sleep(thinkTime);
}

// ========================================
// TEARDOWN PHASE
// ========================================

export function teardown(data) {
  console.log('\n' + '='.repeat(60));
  console.log('LOAD TEST COMPLETED');
  console.log('='.repeat(60));
  console.log(`Total Users Tested: ${data.totalUsers}`);
  console.log('Review detailed metrics above for performance analysis.');
  console.log('='.repeat(60));
}

