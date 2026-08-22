@echo off
REM ============================================================================
REM HealLink - Concurrent Sign-In Load Test Runner (Windows)
REM ============================================================================
REM
REM This script provides easy execution of concurrent sign-in load tests
REM with different profiles and environments.
REM
REM Usage:
REM   run-concurrent-signin.bat [profile] [environment]
REM
REM Examples:
REM   run-concurrent-signin.bat smoke local
REM   run-concurrent-signin.bat load staging
REM   run-concurrent-signin.bat stress local
REM
REM ============================================================================

setlocal enabledelayedexpansion

REM Parse arguments
set PROFILE=%1
set ENVIRONMENT=%2

REM Default values if not provided
if "%PROFILE%"=="" set PROFILE=smoke
if "%ENVIRONMENT%"=="" set ENVIRONMENT=local

REM Validate profile
if not "%PROFILE%"=="smoke" if not "%PROFILE%"=="load" if not "%PROFILE%"=="stress" if not "%PROFILE%"=="soak" (
    echo ERROR: Invalid profile '%PROFILE%'
    echo Valid profiles: smoke, load, stress, soak
    exit /b 1
)

REM Validate environment
if not "%ENVIRONMENT%"=="local" if not "%ENVIRONMENT%"=="staging" if not "%ENVIRONMENT%"=="production" (
    echo ERROR: Invalid environment '%ENVIRONMENT%'
    echo Valid environments: local, staging, production
    exit /b 1
)

REM Check if k6 is installed
where k6 >nul 2>&1
if errorlevel 1 (
    echo ERROR: k6 is not installed or not in PATH
    echo.
    echo Install k6 using:
    echo   choco install k6
    echo.
    echo Or download from: https://k6.io/docs/getting-started/installation/
    exit /b 1
)

REM Display test configuration
echo ============================================================================
echo HealLink - Concurrent Sign-In Load Test
echo ============================================================================
echo Profile:     %PROFILE%
echo Environment: %ENVIRONMENT%
echo ============================================================================
echo.

REM Set output directory
set REPORT_DIR=reports
if not exist "%REPORT_DIR%" mkdir "%REPORT_DIR%"

REM Generate timestamp for report filename
for /f "tokens=2 delims==" %%I in ('wmic os get localdatetime /value') do set datetime=%%I
set TIMESTAMP=%datetime:~0,8%-%datetime:~8,6%

REM Set report filename
set REPORT_FILE=%REPORT_DIR%\concurrent-signin-%PROFILE%-%ENVIRONMENT%-%TIMESTAMP%.json

REM Display test info
echo Running test with:
echo - Load Profile: %PROFILE%
echo - Target Environment: %ENVIRONMENT%
echo - Report: %REPORT_FILE%
echo.
echo Starting in 3 seconds...
timeout /t 3 /nobreak >nul

REM Run k6 test
echo.
echo ============================================================================
echo Test Execution
echo ============================================================================
k6 run ^
  --env ENVIRONMENT=%ENVIRONMENT% ^
  --env LOAD_PROFILE=%PROFILE% ^
  --out json=%REPORT_FILE% ^
  scenarios/concurrentSignIn.js

REM Check exit code
if errorlevel 1 (
    echo.
    echo ============================================================================
    echo TEST FAILED
    echo ============================================================================
    echo The load test failed. Review the output above for details.
    echo Check thresholds and error messages.
    echo.
    echo Common issues:
    echo - API not running or not accessible
    echo - Database connection issues
    echo - Performance thresholds exceeded
    echo - Test data issues
    echo.
    exit /b 1
)

echo.
echo ============================================================================
echo TEST COMPLETED SUCCESSFULLY
echo ============================================================================
echo Report saved to: %REPORT_FILE%
echo.
echo Next steps:
echo 1. Review the metrics above
echo 2. Check the JSON report for detailed data
echo 3. Analyze any warnings or threshold violations
echo.

REM Ask if user wants to run another test
echo.
choice /C YN /M "Run another test"
if errorlevel 2 goto :end
if errorlevel 1 goto :menu

:menu
echo.
echo Select test profile:
echo 1. Smoke (quick validation)
echo 2. Load (standard test)
echo 3. Stress (high load)
echo 4. Soak (long duration)
echo 5. Exit
echo.
choice /C 12345 /M "Select option"

if errorlevel 5 goto :end
if errorlevel 4 (
    set PROFILE=soak
    goto :run
)
if errorlevel 3 (
    set PROFILE=stress
    goto :run
)
if errorlevel 2 (
    set PROFILE=load
    goto :run
)
if errorlevel 1 (
    set PROFILE=smoke
    goto :run
)

:run
REM Generate new timestamp
for /f "tokens=2 delims==" %%I in ('wmic os get localdatetime /value') do set datetime=%%I
set TIMESTAMP=%datetime:~0,8%-%datetime:~8,6%
set REPORT_FILE=%REPORT_DIR%\concurrent-signin-%PROFILE%-%ENVIRONMENT%-%TIMESTAMP%.json

echo.
echo Running %PROFILE% test...
echo.
k6 run ^
  --env ENVIRONMENT=%ENVIRONMENT% ^
  --env LOAD_PROFILE=%PROFILE% ^
  --out json=%REPORT_FILE% ^
  scenarios/concurrentSignIn.js

if errorlevel 1 (
    echo Test failed. See output above.
    pause
    goto :end
)

echo.
echo Test completed. Report: %REPORT_FILE%
goto :menu

:end
echo.
echo Exiting...
endlocal
exit /b 0
