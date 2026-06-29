using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.MsSql;

namespace Registration.IntegrationTests;

/// <summary>
/// Boots the API against a real SQL Server 2019 database and the in-memory message
/// transport, so integration tests exercise the full HTTP pipeline, EF Core
/// persistence and migrations.
/// <para>
/// By default a throwaway SQL Server 2019 container is started via Testcontainers.
/// Set the <c>INTEGRATION_TEST_DB</c> environment variable to a connection string
/// to run against an existing SQL Server instead (useful where Docker-in-test is
/// restricted). The target database is created if missing.
/// </para>
/// </summary>
public sealed class RegistrationApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly string? ExternalConnectionString =
        Environment.GetEnvironmentVariable("INTEGRATION_TEST_DB");

    private readonly MsSqlContainer? _database = ExternalConnectionString is null
        ? new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .WithPassword("Your_strong!Passw0rd")
            .Build()
        : null;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((_, configuration) =>
        {
            configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:Default"] = ResolveConnectionString(),
                ["RabbitMq:UseInMemory"] = "true",
                ["Database:AutoMigrate"] = "true",
            });
        });
    }

    // The container's connection string omits Encrypt; the SqlClient 5.x default
    // (Encrypt=True) can break the login handshake against the container's
    // self-signed cert, so force it off for the test database.
    private string ResolveConnectionString()
    {
        if (ExternalConnectionString is not null)
        {
            return ExternalConnectionString;
        }

        var raw = _database!.GetConnectionString();
        return raw.Contains("Encrypt=", StringComparison.OrdinalIgnoreCase) ? raw : $"{raw};Encrypt=False";
    }

    public async Task InitializeAsync()
    {
        if (_database is not null)
        {
            await _database.StartAsync();
        }
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        if (_database is not null)
        {
            await _database.DisposeAsync();
        }

        await base.DisposeAsync();
    }
}

[CollectionDefinition(nameof(RegistrationApiCollection))]
public sealed class RegistrationApiCollection : ICollectionFixture<RegistrationApiFactory>;
