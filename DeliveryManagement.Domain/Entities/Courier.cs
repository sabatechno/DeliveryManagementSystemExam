using DeliveryManagement.Domain.Common;

namespace DeliveryManagement.Domain.Entities;

public class Courier : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public int MaxCapacity { get; set; } = 10;
    public bool IsAvailable { get; set; } = true;
    public ICollection<CourierZone> CourierZones { get; set; } = new List<CourierZone>();
    public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}