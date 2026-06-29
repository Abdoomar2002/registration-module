using Microsoft.EntityFrameworkCore;
using Registration.Persistence;

namespace Registration.Api.Common;

/// <summary>Applies pending EF Core migrations at startup when enabled (convenient for Docker).</summary>
public static class DatabaseMigrationExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        if (!app.Configuration.GetValue("Database:AutoMigrate", true))
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            logger.LogInformation("Applying database migrations...");
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied.");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Database migration failed.");
            throw;
        }
    }
}
