using DeliveryManagement.Domain.Common;

namespace DeliveryManagement.Domain.Entities;

public class Package : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public int DeliveryId { get; set; }
    public Delivery Delivery { get; set; } = null!;
}