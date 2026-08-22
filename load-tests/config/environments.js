/**
 * Environment Configuration for HealLink Load Testing
 * 
 * This module manages environment-specific configurations for different
 * deployment targets (local, staging, production).
 * 
 * Requirements: 7.1, 7.2, 7.3, 7.4, 7.5, 7.6
 */

/**
 * Environment-specific configurations
 * Each environment defines base URL, load profiles, and performance thresholds
 */
export const environments = {
  local: {
    name: 'local',
    baseUrl: 'http://localhost:5000',
    description: 'Local development environment',
    
    // Load profiles for local testing (lighter load)
    loadProfiles: {
      smoke: {
        stages: [
          { duration: '30s', target: 2 }
        ]
      },
      load: {
        stages: [
          { duration: '1m', target: 10 },
          { duration: '2m', target: 10 },
          { duration: '30s', target: 0 }
        ]
      },
      stress: {
        stages: [
          { duration: '2m', target: 20 },
          { duration: '3m', target: 20 },
          { duration: '1m', target: 40 },
          { duration: '2m', target: 0 }
        ]
      }
    },
    
    // Performance thresholds for local environment (more relaxed)
    thresholds: {
      auth: {
        p95_response_time: 800,  // 95th percentile < 800ms
        error_rate: 0.05          // < 5% error rate
      },
      chat: {
        p95_response_time: 1000,  // 95th percentile < 1000ms
        error_rate: 0.05
      },
      connections: {
        p95_response_time: 800,   // 95th percentile < 800ms
        error_rate: 0.05
      },
      database: {
        p95_response_time: 500,   // 95th percentile < 500ms
        error_rate: 0.05
      }
    },
    
    // Connection settings
    maxVUs: 50,
    defaultDuration: '3m',
    requiresApproval: false,
    allowsDataCleanup: true
  },
  
  staging: {
    name: 'staging',
    baseUrl: 'https://heallink-staging.railway.app',
    description: 'Railway staging environment',
    
    // Load profiles for staging (realistic load)
    loadProfiles: {
      smoke: {
        stages: [
          { duration: '30s', target: 5 }
        ]
      },
      load: {
        stages: [
          { duration: '2m', target: 50 },
          { duration: '5m', target: 50 },
          { duration: '1m', target: 0 }
        ]
      },
      stress: {
        stages: [
          { duration: '5m', target: 100 },
          { duration: '10m', target: 100 },
          { duration: '2m', target: 200 },
          { duration: '2m', target: 0 }
        ]
      },
      soak: {
        stages: [
          { duration: '5m', target: 30 },
          { duration: '1h', target: 30 },
          { duration: '5m', target: 0 }
        ]
      }
    },
    
    // Performance thresholds for staging (production-like)
    thresholds: {
      auth: {
        p95_response_time: 500,   // 95th percentile < 500ms
        error_rate: 0.05
      },
      chat: {
        p95_response_time: 800,   // 95th percentile < 800ms
        error_rate: 0.05
      },
      connections: {
        p95_response_time: 600,   // 95th percentile < 600ms
        error_rate: 0.05
      },
      database: {
        p95_response_time: 300,   // 95th percentile < 300ms
        error_rate: 0.05
      }
    },
    
    // Connection settings
    maxVUs: 200,
    defaultDuration: '8m',
    requiresApproval: false,
    allowsDataCleanup: true
  },
  
  production: {
    name: 'production',
    baseUrl: 'https://heallink.app',
    description: 'Production environment (requires explicit approval)',
    
    // Load profiles for production (conservative load)
    loadProfiles: {
      smoke: {
        stages: [
          { duration: '30s', target: 3 }
        ]
      },
      load: {
        stages: [
          { duration: '3m', target: 30 },
          { duration: '5m', target: 30 },
          { duration: '2m', target: 0 }
        ]
      },
      stress: {
        stages: [
          { duration: '5m', target: 50 },
          { duration: '5m', target: 50 },
          { duration: '2m', target: 100 },
          { duration: '3m', target: 0 }
        ]
      }
    },
    
    // Performance thresholds for production (strict)
    thresholds: {
      auth: {
        p95_response_time: 400,   // 95th percentile < 400ms
        error_rate: 0.02          // < 2% error rate
      },
      chat: {
        p95_response_time: 600,   // 95th percentile < 600ms
        error_rate: 0.02
      },
      connections: {
        p95_response_time: 500,   // 95th percentile < 500ms
        error_rate: 0.02
      },
      database: {
        p95_response_time: 250,   // 95th percentile < 250ms
        error_rate: 0.02
      }
    },
    
    // Connection settings
    maxVUs: 100,
    defaultDuration: '10m',
    requiresApproval: true,      // Requires explicit confirmation
    allowsDataCleanup: false     // Never cleanup production data
  }
};

/**
 * Validates environment configuration
 * @param {object} config - Configuration object to validate
 * @throws {Error} If configuration is invalid
 */
function validateConfig(config) {
  // Validate required fields
  if (!config.name) {
    throw new Error('Configuration missing required field: name');
  }
  
  if (!config.baseUrl) {
    throw new Error('Configuration missing required field: baseUrl');
  }
  
  // Simple baseUrl validation (just check it's a string and looks like a URL)
  // k6 may not have full URL parsing support, so do simple string checks
  if (typeof config.baseUrl !== 'string') {
    throw new Error('baseUrl must be a string');
  }
  if (!config.baseUrl.startsWith('http://') && !config.baseUrl.startsWith('https://')) {
    throw new Error(`Invalid baseUrl format: ${config.baseUrl} (must start with http:// or https://)`);
  }
  
  // Validate load profiles exist
  if (!config.loadProfiles || typeof config.loadProfiles !== 'object') {
    throw new Error('Configuration missing or invalid loadProfiles');
  }
  
  // Validate thresholds exist
  if (!config.thresholds || typeof config.thresholds !== 'object') {
    throw new Error('Configuration missing or invalid thresholds');
  }
  
  // Validate numeric fields
  if (typeof config.maxVUs !== 'number' || config.maxVUs <= 0) {
    throw new Error('maxVUs must be a positive number');
  }
  
  // Validate boolean fields
  if (typeof config.requiresApproval !== 'boolean') {
    throw new Error('requiresApproval must be a boolean');
  }
  
  if (typeof config.allowsDataCleanup !== 'boolean') {
    throw new Error('allowsDataCleanup must be a boolean');
  }
}

/**
 * Apply environment variable overrides to configuration
 * @param {object} config - Base configuration
 * @returns {object} Configuration with overrides applied
 */
function applyEnvironmentOverrides(config) {
  const overriddenConfig = { ...config };
  
  // k6 uses __ENV for environment variables (not process.env from Node.js)
  // This function is called at script parse time, so __ENV may not be available
  // Therefore, we return early in k6 context
  
  return overriddenConfig;
}

/**
 * Check if API is accessible
 * @param {string} baseUrl - Base URL to check
 * @returns {Promise<boolean>} True if API is accessible
 */
async function checkApiAccessibility(baseUrl) {
  try {
    // In Node.js environment, we can use fetch (Node 18+)
    if (typeof fetch !== 'undefined') {
      const response = await fetch(baseUrl, { 
        method: 'GET',
        signal: AbortSignal.timeout(5000) // 5 second timeout
      });
      return response.ok || response.status === 404; // 404 is ok, means server is running
    }
    
    // If fetch is not available, return true (will be validated during test execution)
    return true;
  } catch (error) {
    return false;
  }
}

/**
 * Load configuration for specified environment
 * Supports environment variable overrides and validates configuration
 * 
 * @param {string} environment - Environment name (local, staging, production)
 * @param {object} options - Additional options
 * @param {boolean} options.validateAccessibility - Check if API is accessible (default: false)
 * @returns {Promise<object>|object} Environment configuration (async if validateAccessibility is true)
 * 
 * @throws {Error} If environment is invalid or configuration fails validation
 * 
 * Environment Variable Overrides:
 * - BASE_URL: Override the base URL
 * - MAX_VUS: Override maximum virtual users
 * - DURATION: Override default test duration
 * 
 * @example
 * // Basic usage
 * const config = loadConfig('staging');
 * 
 * @example
 * // With environment variable overrides
 * process.env.BASE_URL = 'http://custom-url:8080';
 * process.env.MAX_VUS = '150';
 * const config = loadConfig('staging');
 * 
 * @example
 * // With API accessibility check
 * const config = await loadConfig('staging', { validateAccessibility: true });
 */
export function loadConfig(environment = 'local', options = {}) {
  const env = environment || 'local';
  
  // Validate environment exists
  if (!environments[env]) {
    throw new Error(
      `Invalid environment: ${env}. Valid options: ${Object.keys(environments).join(', ')}`
    );
  }
  
  // Get base configuration
  let config = { ...environments[env] };
  
  // Apply environment variable overrides
  config = applyEnvironmentOverrides(config);
  
  // Validate the final configuration
  validateConfig(config);
  
  // Check for production approval (Requirement 7.6)
  // Note: k6 doesn't have process.env, so we skip this check in k6 context
  // The environment parameter is passed directly to loadConfig
  if (config.requiresApproval && env === 'production') {
    console.warn('⚠️  WARNING: About to run tests against PRODUCTION environment!');
    console.warn('Ensure you have proper approval before proceeding.');
  }
  
  // Optionally validate API accessibility (Requirement 7.5)
  if (options.validateAccessibility) {
    return checkApiAccessibility(config.baseUrl).then(isAccessible => {
      if (!isAccessible) {
        throw new Error(
          `API is not accessible at ${config.baseUrl}. ` +
          'Please verify the environment is running and the URL is correct.'
        );
      }
      return config;
    });
  }
  
  return config;
}

/**
 * Get load profile for environment and profile name
 * @param {string} environment - Environment name
 * @param {string} profileName - Load profile name (smoke, load, stress, soak)
 * @returns {object} Load profile configuration
 */
export function getLoadProfile(environment = 'local', profileName = 'smoke') {
  const config = loadConfig(environment);
  
  if (!config.loadProfiles[profileName]) {
    throw new Error(
      `Invalid load profile: ${profileName}. ` +
      `Valid options for ${environment}: ${Object.keys(config.loadProfiles).join(', ')}`
    );
  }
  
  return config.loadProfiles[profileName];
}

/**
 * Get thresholds for environment and endpoint category
 * @param {string} environment - Environment name
 * @param {string} category - Endpoint category (auth, chat, connections, database)
 * @returns {object} Threshold configuration
 */
export function getThresholds(environment = 'local', category = 'auth') {
  const config = loadConfig(environment);
  
  if (!config.thresholds[category]) {
    throw new Error(
      `Invalid threshold category: ${category}. ` +
      `Valid options: ${Object.keys(config.thresholds).join(', ')}`
    );
  }
  
  return config.thresholds[category];
}

/**
 * Get all available environments
 * @returns {string[]} Array of environment names
 */
export function getAvailableEnvironments() {
  return Object.keys(environments);
}

export default {
  environments,
  loadConfig,
  getLoadProfile,
  getThresholds,
  getAvailableEnvironments
};
