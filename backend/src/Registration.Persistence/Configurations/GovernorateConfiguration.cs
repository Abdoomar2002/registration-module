using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Registration.Domain.Lookups;
using Registration.Persistence.Seeding;

namespace Registration.Persistence.Configurations;

public sealed class GovernorateConfiguration : IEntityTypeConfiguration<Governorate>
{
    public void Configure(EntityTypeBuilder<Governorate> builder)
    {
        builder.ToTable("Governorates");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).ValueGeneratedNever();

        builder.Property(g => g.NameEn).HasMaxLength(100).IsRequired();
        builder.Property(g => g.NameAr).HasMaxLength(100).IsRequired();
        builder.Property(g => g.IsActive).IsRequired();

        builder.Navigation(g => g.Cities).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasData(LookupSeedData.Governorates);
    }
}
