# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

# Copy the solution file and all project files
COPY ["SmartTech.Marketing.sln", "."]
COPY ["SmartTech.Marketing.WebApi/SmartTech.Marketing.WebApi.csproj", "SmartTech.Marketing.WebApi/"]
COPY ["SmartTech.Marketing.Application/SmartTech.Marketing.Application.csproj", "SmartTech.Marketing.Application/"]
COPY ["SmartTech.Marketing.Core/SmartTech.Marketing.Core.csproj", "SmartTech.Marketing.Core/"]
COPY ["SmartTech.Marketing.Domain/SmartTech.Marketing.Domain.csproj", "SmartTech.Marketing.Domain/"]
COPY ["SmartTech.Marketing.Infrastructure/SmartTech.Marketing.Infrastructure.csproj", "SmartTech.Marketing.Infrastructure/"]
COPY ["SmartTech.Marketing.Persistence/SmartTech.Marketing.Persistence.csproj", "SmartTech.Marketing.Persistence/"]

# Restore dependencies
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR "/src/SmartTech.Marketing.WebApi"
RUN dotnet build "SmartTech.Marketing.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "SmartTech.Marketing.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartTech.Marketing.WebApi.dll", "--environment=Development"]
