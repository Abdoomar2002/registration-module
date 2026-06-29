using MassTransit;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Interfaces;
using Registration.Domain.Lookups;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Persistence;

/// <summary>
/// The EF Core implementation of <see cref="IApplicationDbContext"/>. Besides the
/// domain aggregates and lookups it also hosts the MassTransit transactional
/// outbox tables, so integration events are persisted in the same transaction.
/// </summary>
public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<DomainRegistration> Registrations => Set<DomainRegistration>();

    public DbSet<Governorate> Governorates => Set<Governorate>();

    public DbSet<City> Cities => Set<City>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Transactional outbox / inbox tables (MassTransit EF Core integration).
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();

        base.OnModelCreating(modelBuilder);
    }
}
