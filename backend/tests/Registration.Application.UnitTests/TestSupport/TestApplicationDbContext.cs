using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Interfaces;
using Registration.Domain.Lookups;
using DomainAddress = Registration.Domain.Registrations.Address;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Application.UnitTests.TestSupport;

/// <summary>
/// A minimal EF Core InMemory implementation of <see cref="IApplicationDbContext"/>
/// for fast Application unit tests. The full SQL Server model lives in the
/// Persistence layer; this configures just enough (owned value objects + the
/// address relationship) to exercise handlers and validators.
/// </summary>
public sealed class TestApplicationDbContext : DbContext, IApplicationDbContext
{
    public TestApplicationDbContext(DbContextOptions<TestApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<DomainRegistration> Registrations => Set<DomainRegistration>();

    public DbSet<Governorate> Governorates => Set<Governorate>();

    public DbSet<City> Cities => Set<City>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DomainRegistration>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Ignore(r => r.DomainEvents);
            entity.Ignore(r => r.PrimaryAddress);
            entity.OwnsOne(r => r.Name, name =>
            {
                name.Ignore(n => n.FullName);
                name.Property(n => n.First);
                name.Property(n => n.Middle);
                name.Property(n => n.Last);
            });
            entity.OwnsOne(r => r.Email, email =>
            {
                email.Property(e => e.Value);
                email.Property(e => e.Normalized);
            });
            entity.OwnsOne(r => r.Mobile, mobile => mobile.Property(m => m.Value));
            entity.OwnsOne(r => r.BirthDate, birth => birth.Property(b => b.Value));

            entity.HasMany(r => r.Addresses).WithOne().HasForeignKey("RegistrationId");
            entity.Navigation(r => r.Addresses).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<DomainAddress>(entity => entity.HasKey(a => a.Id));

        modelBuilder.Entity<Governorate>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Id).ValueGeneratedNever();
            entity.Ignore(g => g.Cities);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedNever();
            entity.Ignore(c => c.Governorate);
        });
    }
}
