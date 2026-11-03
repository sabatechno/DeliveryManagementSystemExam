using DeliveryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryManagement.Infrastructure.Persistence.Configurations;

public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
{
    public void Configure(EntityTypeBuilder<Zone> builder)
    {
        builder.HasKey(z => z.Id);
        builder.Property(z => z.Name).IsRequired().HasMaxLength(100);
        builder.Property(z => z.BaseFee).HasColumnType("decimal(18,2)");

        builder.HasOne(z => z.ParentZone)
            .WithMany(z => z.ChildZones)
            .HasForeignKey(z => z.ParentZoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(z => !z.IsDeleted);
    }
}