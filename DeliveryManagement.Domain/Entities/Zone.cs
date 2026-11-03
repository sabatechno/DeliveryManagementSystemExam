using DeliveryManagement.Domain.Common;

namespace DeliveryManagement.Domain.Entities;

public class Zone : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int? ParentZoneId { get; set; }
    public Zone? ParentZone { get; set; }
    public ICollection<Zone> ChildZones { get; set; } = new List<Zone>();
    public decimal BaseFee { get; set; }
    public ICollection<CourierZone> CourierZones { get; set; } = new List<CourierZone>();
}