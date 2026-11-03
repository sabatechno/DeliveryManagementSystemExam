using DeliveryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DeliveryManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Zone> Zones { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Courier> Couriers { get; }
    DbSet<CourierZone> CourierZones { get; }
    DbSet<Delivery> Deliveries { get; }
    DbSet<Package> Packages { get; }
    DbSet<AuditLog> AuditLogs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}