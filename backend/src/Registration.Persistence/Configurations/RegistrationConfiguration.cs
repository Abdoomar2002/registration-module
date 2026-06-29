using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.Domain.Registrations.ValueObjects;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Persistence.Configurations;

public sealed class RegistrationConfiguration : IEntityTypeConfiguration<DomainRegistration>
{
    public void Configure(EntityTypeBuilder<DomainRegistration> builder)
    {
        builder.ToTable("Registrations");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.Ignore(r => r.DomainEvents);
        builder.Ignore(r => r.PrimaryAddress);

        builder.OwnsOne(r => r.Name, name =>
        {
            name.Ignore(n => n.FullName);
            name.Property(n => n.First).HasColumnName("FirstName").HasMaxLength(PersonName.MaxPartLength).IsRequired();
            name.Property(n => n.Middle).HasColumnName("MiddleName").HasMaxLength(PersonName.MaxPartLength);
            name.Property(n => n.Last).HasColumnName("LastName").HasMaxLength(PersonName.MaxPartLength).IsRequired();
        });

        builder.OwnsOne(r => r.Email, email =>
        {
            email.Property(e => e.Value).HasColumnName("Email").HasMaxLength(Email.MaxLength).IsRequired();
            email.Property(e => e.Normalized).HasColumnName("NormalizedEmail").HasMaxLength(Email.MaxLength).IsRequired();
            email.HasIndex(e => e.Normalized).IsUnique().HasDatabaseName("UX_Registrations_NormalizedEmail");
        });

        builder.OwnsOne(r => r.Mobile, mobile =>
        {
            mobile.Property(m => m.Value).HasColumnName("Mobile").HasMaxLength(20).IsRequired();
            mobile.HasIndex(m => m.Value).IsUnique().HasDatabaseName("UX_Registrations_Mobile");
        });

        builder.OwnsOne(r => r.BirthDate, birth =>
            birth.Property(b => b.Value).HasColumnName("BirthDate").IsRequired());

        builder.HasMany(r => r.Addresses)
            .WithOne()
            .HasForeignKey("RegistrationId")
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(r => r.Addresses).UsePropertyAccessMode(PropertyAccessMode.Field);

        // Audit columns.
        builder.Property(r => r.CreatedAtUtc).IsRequired();
        builder.Property(r => r.CreatedBy).HasMaxLength(100);
        builder.Property(r => r.UpdatedBy).HasMaxLength(100);
    }
}
