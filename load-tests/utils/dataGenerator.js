/**
 * Test Data Generator for HealLink Load Testing
 * 
 * Generates synthetic user data for load testing scenarios.
 * Ensures data uniqueness and API validation compliance.
 * 
 * Requirements: 3.1, 3.2, 3.3, 3.4, 3.5, 3.6
 */

/**
 * Simple UUID-like string generator using timestamp and random values
 * k6 doesn't support npm modules, so we generate simple unique IDs
 * @returns {string} UUID-like string
 */
function generateSimpleId() {
  const timestamp = Date.now().toString(16);
  const random = Math.random().toString(16).substr(2, 8);
  const counter = Math.floor(Math.random() * 10000).toString(16);
  return `${timestamp}-${random}-${counter}`;
}

/**
 * Generate test users with unique emails and usernames
 * Requirement 3.1: Generate unique user credentials
 * Requirement 3.2: Ensure email uniqueness
 * Requirement 3.3: Generate valid passwords (complexity requirements)
 * 
 * @param {number} count - Number of users to generate
 * @param {object} options - Generation options
 * @param {string} options.role - User role (Patient, Doctor, Admin)
 * @param {string} options.emailDomain - Email domain for testing
 * @param {boolean} options.includeVariations - Generate password variations
 * @returns {Array} Array of user objects
 */
export function generateUsers(count, options = {}) {
  const {
    role = 'Patient',
    emailDomain = 'loadtest.heallink.local',
    includeVariations = false
  } = options;
  
  const users = [];
  const timestamp = Date.now();
  
  // Password variations for testing different scenarios
  const passwords = includeVariations ? [
    'TestPass123!',
    'SecureP@ss456',
    'LoadTest789#',
    'Str0ng$Pass',
    'Valid@Password1'
  ] : ['TestPass123!'];
  
  for (let i = 0; i < count; i++) {
    const userId = generateSimpleId();
    const userNumber = i + 1;
    const password = passwords[i % passwords.length];
    
    users.push({
      id: userId,
      email: `loadtest.user${userNumber}.${timestamp}@${emailDomain}`,
      username: `loadtest_user_${timestamp}_${userNumber}`,
      password: password,
      role: role,
      // Additional metadata for tracking
      generatedAt: new Date().toISOString(),
      testRun: timestamp,
      sequenceNumber: userNumber
    });
  }
  
  return users;
}

/**
 * Generate test doctor accounts
 * Requirement 3.4: Generate doctor-specific fields (license, syndicate)
 * 
 * @param {number} count - Number of doctors to generate
 * @param {object} options - Generation options
 * @returns {Array} Array of doctor objects
 */
export function generateDoctors(count, options = {}) {
  const { emailDomain = 'loadtest.heallink.local' } = options;
  const users = generateUsers(count, { ...options, role: 'Doctor', emailDomain });
  
  // Add doctor-specific fields
  return users.map((user, index) => ({
    ...user,
    specialization: 'Cardiology',
    practiceLicenseNumber: `LIC-${Date.now()}-${index + 1}`,
    syndicateId: `SYN-${Date.now()}-${index + 1}`,
    isApproved: false // Doctors need admin approval
  }));
}

/**
 * Generate test patient accounts
 * Requirement 3.5: Generate patient-specific test data
 * 
 * @param {number} count - Number of patients to generate
 * @param {object} options - Generation options
 * @returns {Array} Array of patient objects
 */
export function generatePatients(count, options = {}) {
  const { emailDomain = 'loadtest.heallink.local' } = options;
  return generateUsers(count, { ...options, role: 'Patient', emailDomain });
}

export default {
  generateUsers,
  generateDoctors,
  generatePatients
};
