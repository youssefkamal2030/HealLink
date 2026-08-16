# HealLink Load Testing Infrastructure

Comprehensive k6-based load testing infrastructure for the HealLink healthcare platform API.

## 🎯 Overview

This load testing system validates system performance, identifies bottlenecks, and uncovers production-like bugs under realistic load conditions. It tests critical API endpoints including authentication, real-time chat (SignalR), doctor-patient connections, prescriptions, and medical records management.

## 📋 Prerequisites

### Required Software

- **k6** (v0.48+) - [Installation Guide](https://k6.io/docs/getting-started/installation/)
- **Node.js** (v18+) - For utilities and property testing
- **npm** (v9+) - Package management

### Installation

```bash
# Install k6 (Windows - using Chocolatey)
choco install k6

# Install k6 (macOS)
brew install k6

# Install k6 (Linux)
sudo gpg -k
sudo gpg --no-default-keyring --keyring /usr/share/keyrings/k6-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys C5AD17C747E3415A3642D57D77C6C491D6AC1D69
echo "deb [signed-by=/usr/share/keyrings/k6-archive-keyring.gpg] https://dl.k6.io/deb stable main" | sudo tee /etc/apt/sources.list.d/k6.list
sudo apt-get update
sudo apt-get install k6

# Install Node.js dependencies
cd load-tests
npm install
```

## 📁 Project Structure

```
load-tests/
├── scenarios/        # k6 test scenarios (authStorm, chatBurst, etc.)
├── utils/           # Utilities (auth, dataGenerator, metricsCollector)
├── config/          # Environment configurations and thresholds
├── data/            # Generated test data (gitignored)
├── reports/         # Test execution reports (gitignored)
├── package.json     # Node.js dependencies
├── jest.config.js   # Jest configuration for unit/property tests
└── README.md        # This file
```

## 🚀 Quick Start

### 1. Configure Environment

```bash
# Copy example environment file
cp .env.example .env

# Edit .env with your configuration
# Set ENVIRONMENT=local for local testing
```

### 2. Run Your First Load Test

```bash
# Smoke test (quick validation with minimal load)
k6 run scenarios/authStorm.js --env LOAD_PROFILE=smoke

# Full authentication storm test
k6 run scenarios/authStorm.js
```

### 3. View Results

Test results are displayed in the console and saved to `reports/` directory as HTML files.

## 🧪 Available Test Scenarios

| Scenario | Description | Default Load Profile |
|----------|-------------|---------------------|
| **authStorm.js** | Concurrent login requests | 100 VUs, 3 min |
| **chatBurst.js** | Rapid message sending | 50 VUs, 5 min |
| **connectionFlood.js** | Connection requests storm | 30 VUs, 3 min |
| **mixedWorkload.js** | Combined realistic usage | 80 VUs, 10 min |
| **signalrLoad.js** | WebSocket connections | 500 connections |

## 🎛️ Load Profiles

Predefined load profiles for different testing objectives:

- **smoke**: Quick validation (1-5 VUs, 30s)
- **load**: Standard performance test (0→50 VUs, 8 min total)
- **stress**: High load testing (0→200 VUs peak, 16 min)
- **soak**: Stability testing (30 VUs, 1 hour)

Usage:
```bash
k6 run scenarios/authStorm.js --env LOAD_PROFILE=stress
```

## 🌍 Testing Environments

### Local Development
```bash
ENVIRONMENT=local k6 run scenarios/authStorm.js
# Targets: http://localhost:8080
```

### Staging
```bash
ENVIRONMENT=staging k6 run scenarios/authStorm.js
# Targets: Railway/Azure staging environment
```

### Production (Requires Approval)
```bash
ENVIRONMENT=production ALLOW_PRODUCTION_TESTS=true k6 run scenarios/authStorm.js
# ⚠️ Use with caution!
```

## 📊 Performance Thresholds

Automated threshold validation ensures performance standards:

| Endpoint Category | p95 Response Time | Error Rate |
|------------------|------------------|------------|
| Authentication | < 500ms | < 5% |
| Chat Operations | < 800ms | < 5% |
| Connection Requests | < 600ms | < 5% |
| Database Queries | < 300ms | < 5% |

Tests automatically fail if thresholds are exceeded.

## 🧰 Utility Commands

```bash
# Run unit tests
npm test

# Run property-based tests
npm run test:property

# Run tests with coverage
npm run test:coverage

# Lint code
npm run lint
npm run lint:fix

# Generate test data
node utils/dataGenerator.js --count 100

# Clean up test data
node utils/cleanup.js --environment local
```

## 📈 Monitoring & Reporting

### Console Output
Real-time metrics displayed during test execution with color-coded status.

### HTML Reports
Detailed reports generated in `reports/` directory after each test run.

### Grafana Integration (Optional)
Configure InfluxDB output for real-time Grafana dashboards:

```bash
# Set in .env
GRAFANA_URL=http://localhost:3000
INFLUXDB_URL=http://localhost:8086
```

## 🔒 Security Best Practices

- ✅ Store credentials in environment variables, never in code
- ✅ Use synthetic test data only (no real user PII)
- ✅ Passwords and tokens are redacted from logs
- ✅ Production testing requires explicit approval
- ✅ All API requests use HTTPS in staging/production

## 🤖 CI/CD Integration

Load tests run automatically via GitHub Actions:

```yaml
# Triggered on:
- Push to staging branch
- Manual workflow_dispatch

# Default behavior:
- Runs smoke test profile
- Fails build on threshold violations
- Uploads test reports as artifacts
- Sends alerts on failures
```

## 🐛 Troubleshooting

### Common Issues

**1. Connection Timeouts**
```
Error: Request timeout after 30s
Solution: Check if API is running and accessible
```

**2. Authentication Failures**
```
Error: 401 Unauthorized
Solution: Verify credentials in .env file
```

**3. Database Connection Pool Exhaustion**
```
Error: 500 - Connection pool exhausted
Solution: Reduce VU count or increase DB pool size
```

**4. SignalR Connection Failures**
```
Error: WebSocket connection failed
Solution: Check SignalR hub is enabled and accessible
```

## 📚 Additional Documentation

- [Test Results Interpretation Guide](docs/RESULTS_GUIDE.md) *(Coming soon)*
- [Troubleshooting Guide](docs/TROUBLESHOOTING.md) *(Coming soon)*
- [Example Test Scenarios](docs/EXAMPLES.md) *(Coming soon)*
- [k6 Official Documentation](https://k6.io/docs/)

## 🤝 Contributing

When adding new test scenarios:

1. Follow existing naming conventions
2. Include error handling and recovery logic
3. Add performance thresholds
4. Document scenario purpose and load profile
5. Write unit tests for utility functions

## 📝 License

MIT License - See LICENSE file for details

---

**Version**: 1.0.0  
**Last Updated**: 2025-01-XX  
**Maintained By**: HealLink Development Team
