@echo off
echo Running SonarQube analysis for ECommerceApp...
set SONAR_SCANNER_PATH=C:\sonar-scanner\sonar-scanner-5.0.1.3006\bin
set PATH=%PATH%;%SONAR_SCANNER_PATH%
sonar-scanner ^
  -Dsonar.projectKey=ECommerceApp ^
  -Dsonar.host.url=http://localhost:9000 ^
  -Dsonar.token=%SONAR_TOKEN%
if %ERRORLEVEL% NEQ 0 (
  echo SonarQube analysis failed!
  exit /b %ERRORLEVEL%
)
echo Analysis completed. Check results at http://localhost:9000