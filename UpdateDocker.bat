@echo off
SET CONTAINER_NAME=my-smarttech-app
SET IMAGE_NAME=smarttech-marketing-webapi

echo Building Docker image...
docker build -t %IMAGE_NAME% .

echo Checking if %CONTAINER_NAME% is running...
docker ps -q -f name=%CONTAINER_NAME% | findstr /r /c:"." > nul
if %ERRORLEVEL% == 0 (
    echo Stopping running container...
    docker stop %CONTAINER_NAME%
)

echo Checking if %CONTAINER_NAME% exists...
docker ps -aq -f name=%CONTAINER_NAME% | findstr /r /c:"." > nul
if %ERRORLEVEL% == 0 (
    echo Removing existing container...
    docker rm %CONTAINER_NAME%
)

echo Running new container...
docker run -d -p 8083:8080 -p 8084:8081 --name %CONTAINER_NAME% %IMAGE_NAME%

echo Done.
