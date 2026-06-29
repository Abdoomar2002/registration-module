using Microsoft.EntityFrameworkCore;
using Registration.Domain.Lookups;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Application.Common.Interfaces;

/// <summary>
/// The persistence contract owned by the Application layer. The Persistence layer
/// supplies the concrete EF Core implementation, keeping handlers unaware of the
/// specific provider.
/// </summary>
public interface IApplicationDbContext
{
    DbSet<DomainRegistration> Registrations { get; }

    DbSet<Governorate> Governorates { get; }

    DbSet<City> Cities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
