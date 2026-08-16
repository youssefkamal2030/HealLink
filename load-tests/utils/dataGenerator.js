/**
 * Test Data Generator for HealLink Load Testing
 * 
 * Generates synthetic user data for load testing scenarios.
 * Ensures data uniqueness and API validation compliance.
 * 
 * Requirements: 3.1, 3.2, 3.3, 3.4, 3.5, 3.6
 */

import { v4 as uuidv4 } from 'uuid';

// Placeholder for data generation functions
// Will be fully implemented in Task 3.1

/**
 * Generate test users with unique emails and usernames
 * @param {number} count - Number of users to generate
 * @returns {Array} Array of user objects
 */
export function generateUsers(count) {
  // Implementation coming in Task 3.1
  const users = [];
  
  for (let i = 0; i < count; i++) {
    users.push({
      id: uuidv4(),
      email: `loadtest_user_${uuidv4()}@test.heallink.local`,
      username: `loadtest_${Date.now()}_${i}`,
      password: 'TestPass123!',
      role: 'Patient'
    });
  }
  
  return users;
}

/**
 * Generate test doctor accounts
 * @param {number} count - Number of doctors to generate
 * @returns {Array} Array of doctor objects
 */
export function generateDoctors(_count) {
  // Implementation coming in Task 3.1
  return [];
}

/**
 * Generate test patient accounts
 * @param {number} count - Number of patients to generate
 * @returns {Array} Array of patient objects
 */
export function generatePatients(_count) {
  // Implementation coming in Task 3.1
  return [];
}

export default {
  generateUsers,
  generateDoctors,
  generatePatients
};
