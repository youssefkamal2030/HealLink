/**
 * Unit Tests for Environment Configuration Module
 * 
 * Tests the loadConfig() function and environment variable overrides
 * 
 * Requirements: 7.1, 7.2, 7.3, 7.4, 7.5, 7.6
 */

import { describe, it, expect, beforeEach, afterEach } from '@jest/globals';
import { 
  loadConfig, 
  getLoadProfile, 
  getThresholds, 
  getAvailableEnvironments,
  environments 
} from '../environments.js';

describe('Environment Configuration Module', () => {
  let originalEnv;

  beforeEach(() => {
    // Save original environment variables
    originalEnv = { ...process.env };
  });

  afterEach(() => {
    // Restore original environment variables
    process.env = originalEnv;
  });

  describe('loadConfig()', () => {
    it('should load local environment configuration', () => {
      const config = loadConfig('local');
      
      expect(config).toBeDefined();
      expect(config.name).toBe('local');
      expect(config.baseUrl).toBe('http://localhost:8080');
      expect(config.maxVUs).toBe(50);
      expect(config.requiresApproval).toBe(false);
      expect(config.allowsDataCleanup).toBe(true);
    });

    it('should load staging environment configuration', () => {
      const config = loadConfig('staging');
      
      expect(config).toBeDefined();
      expect(config.name).toBe('staging');
      expect(config.baseUrl).toBe('https://heallink-staging.railway.app');
      expect(config.maxVUs).toBe(200);
      expect(config.requiresApproval).toBe(false);
      expect(config.allowsDataCleanup).toBe(true);
    });

    it('should load production environment configuration', () => {
      // Set approval flag for production
      process.env.ALLOW_PRODUCTION_TESTS = 'true';
      
      const config = loadConfig('production');
      
      expect(config).toBeDefined();
      expect(config.name).toBe('production');
      expect(config.baseUrl).toBe('https://heallink.app');
      expect(config.maxVUs).toBe(100);
      expect(config.requiresApproval).toBe(true);
      expect(config.allowsDataCleanup).toBe(false);
    });

    it('should default to local environment when no parameter provided', () => {
      const config = loadConfig();
      
      expect(config.name).toBe('local');
      expect(config.baseUrl).toBe('http://localhost:8080');
    });

    it('should throw error for invalid environment', () => {
      expect(() => loadConfig('invalid')).toThrow('Invalid environment: invalid');
    });

    it('should include load profiles in configuration', () => {
      const config = loadConfig('local');
      
      expect(config.loadProfiles).toBeDefined();
      expect(config.loadProfiles.smoke).toBeDefined();
      expect(config.loadProfiles.load).toBeDefined();
      expect(config.loadProfiles.stress).toBeDefined();
    });

    it('should include thresholds in configuration', () => {
      const config = loadConfig('local');
      
      expect(config.thresholds).toBeDefined();
      expect(config.thresholds.auth).toBeDefined();
      expect(config.thresholds.chat).toBeDefined();
      expect(config.thresholds.connections).toBeDefined();
      expect(config.thresholds.database).toBeDefined();
    });

    it('should require explicit approval for production environment', () => {
      // Without approval flag
      delete process.env.ALLOW_PRODUCTION_TESTS;
      
      expect(() => loadConfig('production')).toThrow(
        'Production environment testing requires explicit confirmation'
      );
    });

    it('should allow production testing when approval flag is set', () => {
      process.env.ALLOW_PRODUCTION_TESTS = 'true';
      
      expect(() => loadConfig('production')).not.toThrow();
    });
  });

  describe('Environment Variable Overrides', () => {
    it('should override baseUrl with BASE_URL environment variable', () => {
      process.env.BASE_URL = 'http://custom-url:9090';
      
      const config = loadConfig('local');
      
      expect(config.baseUrl).toBe('http://custom-url:9090');
    });

    it('should override maxVUs with MAX_VUS environment variable', () => {
      process.env.MAX_VUS = '150';
      
      const config = loadConfig('staging');
      
      expect(config.maxVUs).toBe(150);
    });

    it('should override defaultDuration with DURATION environment variable', () => {
      process.env.DURATION = '10m';
      
      const config = loadConfig('local');
      
      expect(config.defaultDuration).toBe('10m');
    });

    it('should throw error for invalid MAX_VUS format', () => {
      process.env.MAX_VUS = 'not-a-number';
      
      expect(() => loadConfig('local')).toThrow('Invalid MAX_VUS environment variable');
    });

    it('should throw error for negative MAX_VUS', () => {
      process.env.MAX_VUS = '-10';
      
      expect(() => loadConfig('local')).toThrow('Invalid MAX_VUS environment variable');
    });

    it('should throw error for invalid DURATION format', () => {
      process.env.DURATION = 'invalid';
      
      expect(() => loadConfig('local')).toThrow(
        'Invalid DURATION format: invalid. Use format like \'5m\', \'30s\', or \'1h\''
      );
    });

    it('should accept valid DURATION formats', () => {
      const validFormats = ['5m', '30s', '1h', '90s', '2h'];
      
      validFormats.forEach(format => {
        process.env.DURATION = format;
        const config = loadConfig('local');
        expect(config.defaultDuration).toBe(format);
      });
    });

    it('should apply multiple overrides simultaneously', () => {
      process.env.BASE_URL = 'http://override:8888';
      process.env.MAX_VUS = '75';
      process.env.DURATION = '15m';
      
      const config = loadConfig('local');
      
      expect(config.baseUrl).toBe('http://override:8888');
      expect(config.maxVUs).toBe(75);
      expect(config.defaultDuration).toBe('15m');
    });
  });

  describe('Configuration Validation', () => {
    it('should validate baseUrl format', () => {
      // This would be caught by the validation logic
      // Testing indirectly through environment override
      process.env.BASE_URL = 'not-a-valid-url';
      
      expect(() => loadConfig('local')).toThrow('Invalid baseUrl format');
    });

    it('should have valid structure for all environments', () => {
      const envNames = ['local', 'staging', 'production'];
      
      envNames.forEach(envName => {
        if (envName === 'production') {
          process.env.ALLOW_PRODUCTION_TESTS = 'true';
        }
        
        const config = loadConfig(envName);
        
        // Validate required fields
        expect(config.name).toBeDefined();
        expect(config.baseUrl).toBeDefined();
        expect(config.loadProfiles).toBeDefined();
        expect(config.thresholds).toBeDefined();
        expect(config.maxVUs).toBeGreaterThan(0);
        expect(typeof config.requiresApproval).toBe('boolean');
        expect(typeof config.allowsDataCleanup).toBe('boolean');
      });
    });
  });

  describe('getLoadProfile()', () => {
    it('should return smoke profile for local environment', () => {
      const profile = getLoadProfile('local', 'smoke');
      
      expect(profile).toBeDefined();
      expect(profile.stages).toBeDefined();
      expect(Array.isArray(profile.stages)).toBe(true);
    });

    it('should return load profile for staging environment', () => {
      const profile = getLoadProfile('staging', 'load');
      
      expect(profile).toBeDefined();
      expect(profile.stages).toBeDefined();
    });

    it('should throw error for invalid profile name', () => {
      expect(() => getLoadProfile('local', 'invalid')).toThrow('Invalid load profile');
    });

    it('should default to local environment and smoke profile', () => {
      const profile = getLoadProfile();
      
      expect(profile).toBeDefined();
      expect(profile.stages).toBeDefined();
    });
  });

  describe('getThresholds()', () => {
    it('should return auth thresholds for local environment', () => {
      const thresholds = getThresholds('local', 'auth');
      
      expect(thresholds).toBeDefined();
      expect(thresholds.p95_response_time).toBe(800);
      expect(thresholds.error_rate).toBe(0.05);
    });

    it('should return chat thresholds for staging environment', () => {
      const thresholds = getThresholds('staging', 'chat');
      
      expect(thresholds).toBeDefined();
      expect(thresholds.p95_response_time).toBe(800);
      expect(thresholds.error_rate).toBe(0.05);
    });

    it('should return stricter thresholds for production', () => {
      process.env.ALLOW_PRODUCTION_TESTS = 'true';
      
      const authThresholds = getThresholds('production', 'auth');
      
      expect(authThresholds.p95_response_time).toBe(400);
      expect(authThresholds.error_rate).toBe(0.02);
    });

    it('should throw error for invalid threshold category', () => {
      expect(() => getThresholds('local', 'invalid')).toThrow('Invalid threshold category');
    });

    it('should default to local environment and auth category', () => {
      const thresholds = getThresholds();
      
      expect(thresholds).toBeDefined();
      expect(thresholds.p95_response_time).toBeDefined();
      expect(thresholds.error_rate).toBeDefined();
    });
  });

  describe('getAvailableEnvironments()', () => {
    it('should return all available environment names', () => {
      const envs = getAvailableEnvironments();
      
      expect(Array.isArray(envs)).toBe(true);
      expect(envs).toContain('local');
      expect(envs).toContain('staging');
      expect(envs).toContain('production');
      expect(envs.length).toBe(3);
    });
  });

  describe('Threshold Values Per Environment', () => {
    it('should have more relaxed thresholds for local environment', () => {
      const config = loadConfig('local');
      
      expect(config.thresholds.auth.p95_response_time).toBe(800);
      expect(config.thresholds.chat.p95_response_time).toBe(1000);
      expect(config.thresholds.connections.p95_response_time).toBe(800);
    });

    it('should have production-like thresholds for staging', () => {
      const config = loadConfig('staging');
      
      expect(config.thresholds.auth.p95_response_time).toBe(500);
      expect(config.thresholds.chat.p95_response_time).toBe(800);
      expect(config.thresholds.connections.p95_response_time).toBe(600);
    });

    it('should have strict thresholds for production', () => {
      process.env.ALLOW_PRODUCTION_TESTS = 'true';
      const config = loadConfig('production');
      
      expect(config.thresholds.auth.p95_response_time).toBe(400);
      expect(config.thresholds.chat.p95_response_time).toBe(600);
      expect(config.thresholds.connections.p95_response_time).toBe(500);
      expect(config.thresholds.auth.error_rate).toBe(0.02);
    });
  });

  describe('Load Profile Stages', () => {
    it('should have valid stage structure', () => {
      const profile = getLoadProfile('local', 'smoke');
      
      profile.stages.forEach(stage => {
        expect(stage.duration).toBeDefined();
        expect(stage.target).toBeDefined();
        expect(typeof stage.target).toBe('number');
      });
    });

    it('should have lighter load profiles for local', () => {
      const loadProfile = getLoadProfile('local', 'load');
      const maxTarget = Math.max(...loadProfile.stages.map(s => s.target));
      
      expect(maxTarget).toBeLessThanOrEqual(10);
    });

    it('should have realistic load profiles for staging', () => {
      const loadProfile = getLoadProfile('staging', 'load');
      const maxTarget = Math.max(...loadProfile.stages.map(s => s.target));
      
      expect(maxTarget).toBeGreaterThanOrEqual(50);
    });

    it('should have soak test profile for staging', () => {
      const soakProfile = getLoadProfile('staging', 'soak');
      
      expect(soakProfile).toBeDefined();
      expect(soakProfile.stages.length).toBeGreaterThan(0);
      
      // Check that soak test has extended duration
      const hasSoakStage = soakProfile.stages.some(stage => 
        stage.duration.includes('h') || 
        (stage.duration.includes('m') && parseInt(stage.duration) >= 60)
      );
      expect(hasSoakStage).toBe(true);
    });
  });

  describe('Environment-Specific Settings', () => {
    it('should configure local for data cleanup', () => {
      const config = loadConfig('local');
      expect(config.allowsDataCleanup).toBe(true);
    });

    it('should configure staging for data cleanup', () => {
      const config = loadConfig('staging');
      expect(config.allowsDataCleanup).toBe(true);
    });

    it('should prevent data cleanup in production', () => {
      process.env.ALLOW_PRODUCTION_TESTS = 'true';
      const config = loadConfig('production');
      expect(config.allowsDataCleanup).toBe(false);
    });

    it('should not require approval for non-production environments', () => {
      const localConfig = loadConfig('local');
      const stagingConfig = loadConfig('staging');
      
      expect(localConfig.requiresApproval).toBe(false);
      expect(stagingConfig.requiresApproval).toBe(false);
    });
  });
});
