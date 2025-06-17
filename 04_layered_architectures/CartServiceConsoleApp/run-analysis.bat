@echo off
echo Running SonarQube analysis for ECommerceApp...
set SONAR_TOKEN=%SONAR_TOKEN%

:: Begin SonarScanner analysis
dotnet sonarscanner begin ^
  /k:"ECommerceApp" ^
  /d:sonar.host.url="http://localhost:9000" ^
  /d:sonar.token="%SONAR_TOKEN%" ^
  /d:sonar.cs.opencover.reportsPaths="coverage.opencover.xml"

:: Build the solution
dotnet build ECommerceApp.sln

:: End SonarScanner analysis
dotnet sonarscanner end /d:sonar.token="%SONAR_TOKEN%"

if %ERRORLEVEL% NEQ 0 (
  echo SonarQube analysis failed!
  exit /b %ERRORLEVEL%
)

echo Analysis completed. Check results at http://localhost:9000