/**
 * Authentication and Token Management for HealLink Load Testing
 * 
 * This module provides authentication utilities for k6 load tests including:
 * - JWT token generation via login endpoint
 * - Token validation (format, expiration, claims)
 * - User claims extraction
 * - Token caching per virtual user
 * - Error handling for authentication failures
 * 
 * Requirements: 5.1, 5.2, 5.3, 5.4, 5.5, 5.6
 */

import http from 'k6/http';
import { check } from 'k6';
import encoding from 'k6/encoding';

/**
 * In-memory token cache for virtual users
 * Key: user email or unique identifier
 * Value: { token, expiresAt, claims }
 */
const tokenCache = new Map();

/**
 * Parse JWT token and extract payload
 * @param {string} token - JWT token string
 * @returns {object|null} Decoded payload or null if invalid
 */
function parseJWT(token) {
  if (!token || typeof token !== 'string') {
    return null;
  }
  
  const parts = token.split('.');
  if (parts.length !== 3) {
    return null;
  }
  
  try {
    // Decode the payload (second part)
    const payload = encoding.b64decode(parts[1], 'rawstd', 's');
    return JSON.parse(payload);
  } catch (error) {
    console.error(`Failed to parse JWT token: ${error.message}`);
    return null;
  }
}

/**
 * Validate JWT token format
 * Checks if token follows standard JWT structure (header.payload.signature)
 * 
 * Requirement 5.2: Validate token is properly formatted JWT
 * 
 * @param {string} token - JWT token to validate
 * @returns {boolean} True if token format is valid
 */
export function isValidJWTFormat(token) {
  if (!token || typeof token !== 'string') {
    return false;
  }
  
  // JWT must have exactly 3 parts separated by dots
  const parts = token.split('.');
  if (parts.length !== 3) {
    return false;
  }
  
  // Each part must be non-empty
  return parts.every(part => part.length > 0);
}

/**
 * Check if JWT token is expired
 * 
 * Requirement 5.4: Validate token is not expired
 * 
 * @param {string} token - JWT token to check
 * @returns {boolean} True if token is expired
 */
export function isTokenExpired(token) {
  const payload = parseJWT(token);
  
  if (!payload || !payload.exp) {
    return true; // Treat invalid tokens as expired
  }
  
  // exp is in seconds, Date.now() is in milliseconds
  const expirationTime = payload.exp * 1000;
  const currentTime = Date.now();
  
  return currentTime >= expirationTime;
}

/**
 * Extract user claims from JWT token
 * 
 * Requirement 5.3: Parse userId and role from token
 * 
 * @param {string} token - JWT token
 * @returns {object|null} Object with userId and role, or null if extraction fails
 * 
 * @example
 * const claims = extractUserClaims(token);
 * // Returns: { userId: "guid-string", role: "Patient" }
 */
export function extractUserClaims(token) {
  const payload = parseJWT(token);
  
  if (!payload) {
    return null;
  }
  
  // HealLink JWT tokens use standard claims
  // sub (subject) contains the userId
  // role contains the user role (Patient, Doctor, Admin)
  const userId = payload.sub || payload.nameid || payload.userId;
  const role = payload.role || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  
  if (!userId) {
    console.warn('Token missing userId claim (sub/nameid/userId)');
    return null;
  }
  
  return {
    userId: userId,
    role: role || 'Unknown',
    email: payload.email || null,
    username: payload.username || payload.unique_name || null
  };
}

/**
 * Validate JWT token comprehensively
 * 
 * Requirements: 5.2, 5.3, 5.4
 * 
 * @param {string} token - JWT token to validate
 * @returns {object} Validation result with isValid flag and details
 * 
 * @example
 * const validation = validateToken(token);
 * if (validation.isValid) {
 *   console.log('Token is valid', validation.claims);
 * }
 */
export function validateToken(token) {
  const validation = {
    isValid: false,
    hasValidFormat: false,
    isExpired: false,
    hasClaims: false,
    claims: null,
    errors: []
  };
  
  // Check format (Requirement 5.2)
  validation.hasValidFormat = isValidJWTFormat(token);
  if (!validation.hasValidFormat) {
    validation.errors.push('Invalid JWT format');
    return validation;
  }
  
  // Check expiration (Requirement 5.4)
  validation.isExpired = isTokenExpired(token);
  if (validation.isExpired) {
    validation.errors.push('Token is expired');
    return validation;
  }
  
  // Extract claims (Requirement 5.3)
  validation.claims = extractUserClaims(token);
  validation.hasClaims = validation.claims !== null && validation.claims.userId !== undefined;
  
  if (!validation.hasClaims) {
    validation.errors.push('Token missing required claims (userId)');
    return validation;
  }
  
  // All checks passed
  validation.isValid = true;
  return validation;
}

/**
 * Authenticate user and obtain JWT token
 * 
 * Requirement 5.1: Obtain Auth_Token via login endpoint
 * Requirement 5.5: Log failure and skip scenario execution on 401
 * Requirement 5.6: Maintain isolated tokens per virtual user
 * 
 * @param {string} baseUrl - API base URL
 * @param {string} email - User email
 * @param {string} password - User password
 * @param {object} options - Additional options
 * @param {boolean} options.useCache - Use cached token if available (default: true)
 * @param {Array} options.fallbackUsers - Fallback users to try on auth failure
 * @returns {object|null} Authentication result with token and claims, or null on failure
 * 
 * @example
 * const auth = login(config.baseUrl, 'patient@test.com', 'password123');
 * if (auth) {
 *   console.log('Logged in as:', auth.claims.userId);
 *   // Use auth.token in subsequent requests
 * }
 * 
 * @example
 * // With fallback users
 * const auth = login(baseUrl, email, password, {
 *   fallbackUsers: [
 *     { email: 'user2@test.com', password: 'pass2' },
 *     { email: 'user3@test.com', password: 'pass3' }
 *   ]
 * });
 */
export function login(baseUrl, email, password, options = {}) {
  const { useCache = true, fallbackUsers = [] } = options;
  
  // Check cache first (Requirement 5.6)
  if (useCache && tokenCache.has(email)) {
    const cached = tokenCache.get(email);
    
    // Return cached token if still valid
    if (!isTokenExpired(cached.token)) {
      return cached;
    }
    
    // Remove expired token from cache
    tokenCache.delete(email);
  }
  
  // Prepare login request
  const loginUrl = `${baseUrl}/api/Auth/login`;
  const payload = JSON.stringify({
    Email: email,
    Password: password
  });
  
  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
    timeout: '30s',
    tags: { name: 'auth_login' }
  };
  
  // Make login request (Requirement 5.1)
  const response = http.post(loginUrl, payload, params);
  
  // Validate response
  const loginSuccess = check(response, {
    'login status is 200': (r) => r.status === 200,
    'login response has token': (r) => {
      if (r.status !== 200) return false;
      try {
        const body = JSON.parse(r.body);
        return body.token !== undefined && body.token !== null;
      } catch (e) {
        return false;
      }
    }
  });
  
  if (!loginSuccess) {
    console.error(`Authentication failed for ${email}: HTTP ${response.status}`);
    
    // Requirement 5.5: Handle 401 responses with fallback users
    if (response.status === 401) {
      console.warn(`Authentication failed (401) for ${email}, trying fallback users...`);
      
      // Try fallback users
      for (const fallbackUser of fallbackUsers) {
        console.log(`Attempting fallback authentication with ${fallbackUser.email}`);
        const fallbackAuth = login(baseUrl, fallbackUser.email, fallbackUser.password, {
          useCache,
          fallbackUsers: [] // Prevent infinite recursion
        });
        
        if (fallbackAuth) {
          console.log(`Successfully authenticated with fallback user ${fallbackUser.email}`);
          return fallbackAuth;
        }
      }
      
      console.error(`All authentication attempts failed for ${email} and ${fallbackUsers.length} fallback users`);
    }
    
    return null;
  }
  
  // Parse response
  let responseBody;
  try {
    responseBody = JSON.parse(response.body);
  } catch (error) {
    console.error(`Failed to parse login response: ${error.message}`);
    return null;
  }
  
  const token = responseBody.token;
  
  if (!token) {
    console.error('Login response missing token field');
    return null;
  }
  
  // Validate token (Requirements 5.2, 5.3, 5.4)
  const validation = validateToken(token);
  
  if (!validation.isValid) {
    console.error(`Received invalid token for ${email}: ${validation.errors.join(', ')}`);
    return null;
  }
  
  // Create auth result
  const authResult = {
    token: token,
    claims: validation.claims,
    expiresAt: validation.claims ? parseJWT(token).exp * 1000 : null,
    email: email
  };
  
  // Cache token (Requirement 5.6)
  if (useCache) {
    tokenCache.set(email, authResult);
  }
  
  return authResult;
}

/**
 * Get cached token for user
 * @param {string} email - User email
 * @returns {object|null} Cached authentication result or null
 */
export function getCachedToken(email) {
  if (!tokenCache.has(email)) {
    return null;
  }
  
  const cached = tokenCache.get(email);
  
  // Return null if expired
  if (isTokenExpired(cached.token)) {
    tokenCache.delete(email);
    return null;
  }
  
  return cached;
}

/**
 * Clear cached token for user
 * @param {string} email - User email
 */
export function clearCachedToken(email) {
  tokenCache.delete(email);
}

/**
 * Clear all cached tokens
 */
export function clearAllTokens() {
  tokenCache.clear();
}

/**
 * Get authorization header for authenticated requests
 * @param {string} token - JWT token
 * @returns {object} Headers object with Authorization
 */
export function getAuthHeaders(token) {
  return {
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json'
  };
}

export default {
  login,
  validateToken,
  isValidJWTFormat,
  isTokenExpired,
  extractUserClaims,
  getCachedToken,
  clearCachedToken,
  clearAllTokens,
  getAuthHeaders
};
