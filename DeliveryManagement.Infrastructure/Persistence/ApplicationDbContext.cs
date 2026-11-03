using DeliveryManagement.Application.Common.Interfaces;
using DeliveryManagement.Domain.Common;
using DeliveryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace DeliveryManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService? _currentUserService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService? currentUserService = null) : base(options)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Zone> Zones => Set<Zone>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Courier> Couriers => Set<Courier>();
    public DbSet<CourierZone> CourierZones => Set<CourierZone>();
    public DbSet<Delivery> Deliveries => Set<Delivery>();
    public DbSet<Package> Packages => Set<Package>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Audit Logging تلقائي
        var auditEntries = new List<AuditLog>();
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                auditEntries.Add(new AuditLog
                {
                    EntityName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    UserId = _currentUserService?.UserId,
                    IpAddress = _currentUserService?.IpAddress,
                    NewValues = entry.State != EntityState.Deleted
                        ? System.Text.Json.JsonSerializer.Serialize(entry.CurrentValues.ToObject())
                        : null,
                    OldValues = entry.State != EntityState.Added
                        ? System.Text.Json.JsonSerializer.Serialize(entry.OriginalValues.ToObject())
                        : null
                });
            }
        }

        if (auditEntries.Any())
            AuditLogs.AddRange(auditEntries);

        return await base.SaveChangesAsync(cancellationToken);
    }
}