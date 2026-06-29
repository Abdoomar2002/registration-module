using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Registration.Persistence;

/// <summary>
/// Design-time factory used by `dotnet ef` so migrations can be created/applied
/// without booting the API. The connection string is only needed for
/// `database update`; `migrations add` builds the model offline.
/// </summary>
public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("REGISTRATION_DB_CONNECTION")
            ?? "Server=localhost,1433;Database=RegistrationDb;User Id=sa;Password=Your_strong!Passw0rd;TrustServerCertificate=True;Encrypt=False";

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connectionString, sql =>
                sql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            .Options;

        return new ApplicationDbContext(options);
    }
}
