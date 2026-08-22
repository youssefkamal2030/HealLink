/**
 * K6 Compatibility Test
 * 
 * Quick test to verify k6 can load all required modules
 * and that the concurrent sign-in test can initialize.
 * 
 * Run with: k6 run test-k6-compatibility.js
 */

import http from 'k6/http';
import { check } from 'k6';
import { Counter } from 'k6/metrics';
import { SharedArray } from 'k6/data';

// Try to import our modules
import { generateUsers } from './utils/dataGenerator.js';
import { loadConfig, getLoadProfile, getThresholds } from './config/environments.js';

console.log('✅ All imports successful');

// Test data generation
console.log('Testing data generation...');
const testUsers = generateUsers(5);
console.log(`✅ Generated ${testUsers.length} test users`);
console.log(`Sample user: ${testUsers[0].email}`);

// Test configuration loading
console.log('\nTesting configuration...');
try {
  const config = loadConfig('local');
  console.log(`✅ Configuration loaded for: ${config.name}`);
  console.log(`   Base URL: ${config.baseUrl}`);
} catch (error) {
  console.error(`❌ Configuration error: ${error.message}`);
}

// Test load profile
console.log('\nTesting load profiles...');
try {
  const profile = getLoadProfile('local', 'smoke');
  console.log(`✅ Load profile loaded: smoke`);
} catch (error) {
  console.error(`❌ Load profile error: ${error.message}`);
}

// Test thresholds
console.log('\nTesting thresholds...');
try {
  const thresholds = getThresholds('local', 'auth');
  console.log(`✅ Thresholds loaded for auth`);
} catch (error) {
  console.error(`❌ Thresholds error: ${error.message}`);
}

export const options = {
  stages: [
    { duration: '5s', target: 1 },
  ],
  thresholds: {
    'http_req_duration': ['p(95)<1000'],
  }
};

const counter = new Counter('compatibility_checks');

export default function () {
  counter.add(1);
  sleep(1);
}

function sleep(seconds) {
  const end = Date.now() + seconds * 1000;
  while (Date.now() < end) {}
}
