# Build context is the repository root.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restore first (better layer caching) using the project graph + central package files.
COPY global.json ./
COPY backend/Directory.Build.props backend/Directory.Packages.props backend/Registration.sln ./backend/
COPY backend/src ./backend/src
COPY backend/tests ./backend/tests
WORKDIR /src/backend
RUN dotnet restore src/Registration.Api/Registration.Api.csproj

RUN dotnet publish src/Registration.Api/Registration.Api.csproj \
    -c Release -o /app/publish --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# curl is used by the container healthcheck against /health.
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

HEALTHCHECK --interval=15s --timeout=5s --start-period=30s --retries=10 \
    CMD curl -fsS http://localhost:8080/health || exit 1

USER $APP_UID
ENTRYPOINT ["dotnet", "Registration.Api.dll"]
