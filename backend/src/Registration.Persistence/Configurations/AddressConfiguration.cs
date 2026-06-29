using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.Domain.Lookups;
using DomainAddress = Registration.Domain.Registrations.Address;

namespace Registration.Persistence.Configurations;

public sealed class AddressConfiguration : IEntityTypeConfiguration<DomainAddress>
{
    public void Configure(EntityTypeBuilder<DomainAddress> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property(a => a.Street).HasMaxLength(DomainAddress.StreetMaxLength).IsRequired();
        builder.Property(a => a.BuildingNumber).HasMaxLength(DomainAddress.BuildingNumberMaxLength).IsRequired();
        builder.Property(a => a.FlatNumber).HasMaxLength(DomainAddress.FlatNumberMaxLength).IsRequired();
        builder.Property(a => a.IsPrimary).IsRequired();
        builder.Property(a => a.GovernorateId).IsRequired();
        builder.Property(a => a.CityId).IsRequired();

        // Referential integrity to the lookups (Restrict: lookups are never deleted by a registration).
        builder.HasOne<Governorate>()
            .WithMany()
            .HasForeignKey(a => a.GovernorateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(a => a.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex("RegistrationId");
        builder.HasIndex(a => a.GovernorateId);
        builder.HasIndex(a => a.CityId);
    }
}
