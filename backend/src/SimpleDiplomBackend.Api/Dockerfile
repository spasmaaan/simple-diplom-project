# To build this dockerfile, run the following command from the startup project directory:
# docker build -tag net7webapi .

# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG Configuration=Release
ENV DOTNET_CLI_TELEMETRY_OUTPUT=true \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
WORKDIR /app

# Copy the csproj files and restore dependencies for each layer
COPY *.sln .
COPY src/SimpleDiplomBackend.Api/*.csproj ./src/SimpleDiplomBackend.Api/
COPY src/SimpleDiplomBackend.Domain/*.csproj ./src/SimpleDiplomBackend.Domain/
COPY src/SimpleDiplomBackend.Application/*.csproj ./src/SimpleDiplomBackend.Application/
COPY src/SimpleDiplomBackend.Infrastructure/*.csproj ./src/SimpleDiplomBackend.Infrastructure/
COPY src/SimpleDiplomBackend.Persistence/*.csproj ./src/SimpleDiplomBackend.Persistence/

RUN dotnet restore

# Copy the project files 
COPY src/SimpleDiplomBackend.Api/. ./src/SimpleDiplomBackend.Api/
COPY src/SimpleDiplomBackend.Domain/. ./src/SimpleDiplomBackend.Domain/
COPY src/SimpleDiplomBackend.Application/. ./src/SimpleDiplomBackend.Application/
COPY src/SimpleDiplomBackend.Infrastructure/. ./src/SimpleDiplomBackend.Infrastructure/
COPY src/SimpleDiplomBackend.Persistence/. ./src/SimpleDiplomBackend.Persistence/

# Build and publish app
WORKDIR /app/src/SimpleDiplomBackend.Api
RUN dotnet publish -c $Configuration -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
# install cultures
RUN apk add --no-cache icu-libs
# Disbale the invariant mode "set in base image"
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
EXPOSE 80 443
COPY --from=build-env /app/src/SimpleDiplomBackend.Api/out ./
ENTRYPOINT ["dotnet", "SimpleDiplomBackend.Api.dll"]