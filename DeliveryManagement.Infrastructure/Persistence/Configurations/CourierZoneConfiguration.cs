using DeliveryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryManagement.Infrastructure.Persistence.Configurations;

public class CourierZoneConfiguration : IEntityTypeConfiguration<CourierZone>
{
    public void Configure(EntityTypeBuilder<CourierZone> builder)
    {
        builder.HasKey(cz => new { cz.CourierId, cz.ZoneId });

        builder.HasOne(cz => cz.Courier)
            .WithMany(c => c.CourierZones)
            .HasForeignKey(cz => cz.CourierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cz => cz.Zone)
            .WithMany(z => z.CourierZones)
            .HasForeignKey(cz => cz.ZoneId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}