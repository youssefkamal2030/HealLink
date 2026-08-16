# Environment Configuration Usage Guide

## Overview

The `environments.js` module provides comprehensive configuration management for load testing across different deployment environments (local, staging, production).

## Basic Usage

### Loading Configuration

```javascript
import { loadConfig } from './config/environments.js';

// Load configuration for specific environment
const config = loadConfig('staging');

console.log(config.baseUrl);  // https://heallink-staging.railway.app
console.log(config.maxVUs);   // 200
```

### Using in k6 Test Scenarios

```javascript
import http from 'k6/http';
import { loadConfig, getLoadProfile, getThresholds } from './config/environments.js';

// Determine environment from k6 environment variable or default to local
const ENV = __ENV.ENVIRONMENT || 'local';
const config = loadConfig(ENV);
const loadProfile = getLoadProfile(ENV, 'smoke');
const thresholds = getThresholds(ENV, 'auth');

// Configure k6 options using environment configuration
export const options = {
  stages: loadProfile.stages,
  thresholds: {
    'http_req_duration': [`p(95)<${thresholds.p95_response_time}`],
    'http_req_failed': [`rate<${thresholds.error_rate}`],
  },
};

// Use baseUrl in test
export default function () {
  const response = http.get(`${config.baseUrl}/api/Auth/login`);
  // ... test logic
}
```

## Environment Variable Overrides

Override configuration values at runtime using environment variables:

```bash
# Override base URL
BASE_URL=http://custom-server:8080 k6 run scenarios/authStorm.js

# Override max virtual users
MAX_VUS=150 k6 run scenarios/authStorm.js

# Override test duration
DURATION=10m k6 run scenarios/authStorm.js

# Specify environment
ENVIRONMENT=staging k6 run scenarios/authStorm.js

# Multiple overrides
BASE_URL=http://localhost:9090 MAX_VUS=200 DURATION=5m k6 run scenarios/authStorm.js
```

## Available Functions

### `loadConfig(environment, options)`

Loads configuration for the specified environment with optional validation.

**Parameters:**
- `environment` (string): Environment name ('local', 'staging', 'production')
- `options` (object): Optional configuration
  - `validateAccessibility` (boolean): Check if API is accessible before running tests

**Returns:** Configuration object

**Example:**
```javascript
// Basic usage
const config = loadConfig('staging');

// With API accessibility check (async)
const config = await loadConfig('staging', { validateAccessibility: true });
```

### `getLoadProfile(environment, profileName)`

Returns the load profile configuration for a specific environment and profile.

**Parameters:**
- `environment` (string): Environment name
- `profileName` (string): Profile name ('smoke', 'load', 'stress', 'soak')

**Returns:** Load profile with stages array

**Example:**
```javascript
const smokeTest = getLoadProfile('local', 'smoke');
const loadTest = getLoadProfile('staging', 'load');
const stressTest = getLoadProfile('production', 'stress');
```

### `getThresholds(environment, category)`

Returns performance thresholds for a specific environment and endpoint category.

**Parameters:**
- `environment` (string): Environment name
- `category` (string): Endpoint category ('auth', 'chat', 'connections', 'database')

**Returns:** Threshold object with p95_response_time and error_rate

**Example:**
```javascript
const authThresholds = getThresholds('staging', 'auth');
const chatThresholds = getThresholds('staging', 'chat');

console.log(authThresholds.p95_response_time);  // 500
console.log(authThresholds.error_rate);         // 0.05
```

### `getAvailableEnvironments()`

Returns array of all available environment names.

**Example:**
```javascript
const environments = getAvailableEnvironments();
console.log(environments);  // ['local', 'staging', 'production']
```

## Environment Configurations

### Local Environment
- **Base URL:** `http://localhost:8080`
- **Max VUs:** 50
- **Profiles:** smoke, load, stress
- **Thresholds:** Relaxed (p95 < 800ms for auth)
- **Data Cleanup:** Enabled

### Staging Environment
- **Base URL:** `https://heallink-staging.railway.app`
- **Max VUs:** 200
- **Profiles:** smoke, load, stress, soak
- **Thresholds:** Production-like (p95 < 500ms for auth)
- **Data Cleanup:** Enabled

### Production Environment
- **Base URL:** `https://heallink.app`
- **Max VUs:** 100
- **Profiles:** smoke, load, stress
- **Thresholds:** Strict (p95 < 400ms for auth, < 2% error rate)
- **Data Cleanup:** Disabled
- **Requires Approval:** Yes (set `ALLOW_PRODUCTION_TESTS=true`)

## Load Profiles

### Smoke Test
Quick validation with minimal load (1-5 VUs for 30 seconds).

```javascript
const profile = getLoadProfile('staging', 'smoke');
// stages: [{ duration: '30s', target: 5 }]
```

### Load Test
Sustained load to test normal operating conditions.

```javascript
const profile = getLoadProfile('staging', 'load');
// stages: ramp-up to 50 VUs, hold 5 min, ramp-down
```

### Stress Test
High load to find breaking points and bottlenecks.

```javascript
const profile = getLoadProfile('staging', 'stress');
// stages: ramp to 100 VUs, hold, spike to 200, ramp-down
```

### Soak Test
Long-running test to detect memory leaks and stability issues (staging only).

```javascript
const profile = getLoadProfile('staging', 'soak');
// stages: 30 VUs for 1 hour
```

## Performance Thresholds

Different environments have different performance expectations:

| Category    | Local (ms) | Staging (ms) | Production (ms) |
|-------------|------------|--------------|-----------------|
| Auth        | 800        | 500          | 400             |
| Chat        | 1000       | 800          | 600             |
| Connections | 800        | 600          | 500             |
| Database    | 500        | 300          | 250             |

Error rate thresholds:
- **Local/Staging:** < 5%
- **Production:** < 2%

## Production Safety

Running tests against production requires explicit approval:

```bash
# This will fail without approval
k6 run -e ENVIRONMENT=production scenarios/authStorm.js
# Error: Production environment testing requires explicit confirmation

# Set approval flag
ALLOW_PRODUCTION_TESTS=true ENVIRONMENT=production k6 run scenarios/authStorm.js
# ⚠️  WARNING: Running load tests against PRODUCTION environment!
```

## API Accessibility Validation

Optionally validate that the target API is accessible before running tests:

```javascript
try {
  const config = await loadConfig('staging', { validateAccessibility: true });
  console.log('API is accessible');
} catch (error) {
  console.error('API is not accessible:', error.message);
  // Don't run tests
}
```

## Complete Example

```javascript
import http from 'k6/http';
import { check, sleep } from 'k6';
import { loadConfig, getLoadProfile, getThresholds } from './config/environments.js';

// Get environment from k6 __ENV or default to local
const ENV = __ENV.ENVIRONMENT || 'local';
const PROFILE = __ENV.PROFILE || 'smoke';

// Load configuration
const config = loadConfig(ENV);
const loadProfile = getLoadProfile(ENV, PROFILE);
const authThresholds = getThresholds(ENV, 'auth');

// Log configuration
console.log(`Running ${PROFILE} test against ${config.name} environment`);
console.log(`Base URL: ${config.baseUrl}`);
console.log(`Max VUs: ${config.maxVUs}`);

// Configure k6 test options
export const options = {
  stages: loadProfile.stages,
  thresholds: {
    'http_req_duration': [`p(95)<${authThresholds.p95_response_time}`],
    'http_req_failed': [`rate<${authThresholds.error_rate}`],
  },
};

// Test scenario
export default function () {
  const payload = JSON.stringify({
    email: 'test@example.com',
    password: 'TestPass123!',
  });

  const params = {
    headers: { 'Content-Type': 'application/json' },
  };

  const response = http.post(
    `${config.baseUrl}/api/Auth/login`,
    payload,
    params
  );

  check(response, {
    'status is 200': (r) => r.status === 200,
    'token returned': (r) => r.json('token') !== undefined,
  });

  sleep(1);
}
```

Run the test:

```bash
# Local smoke test (default)
k6 run scenarios/authStorm.js

# Staging load test
k6 run -e ENVIRONMENT=staging -e PROFILE=load scenarios/authStorm.js

# With overrides
BASE_URL=http://custom:8080 MAX_VUS=100 k6 run scenarios/authStorm.js
```

## Troubleshooting

### Invalid environment error
```
Error: Invalid environment: dev. Valid options: local, staging, production
```
**Solution:** Use one of the valid environment names.

### Production requires approval
```
Error: Production environment testing requires explicit confirmation
```
**Solution:** Set `ALLOW_PRODUCTION_TESTS=true` environment variable.

### Invalid MAX_VUS
```
Error: Invalid MAX_VUS environment variable: abc
```
**Solution:** Provide a positive integer value for MAX_VUS.

### Invalid DURATION format
```
Error: Invalid DURATION format: 5minutes. Use format like '5m', '30s', or '1h'
```
**Solution:** Use format: `<number><unit>` where unit is `s` (seconds), `m` (minutes), or `h` (hours).

### API not accessible
```
Error: API is not accessible at http://localhost:8080
```
**Solution:** Ensure the HealLink API is running at the specified URL, or use a different environment.
