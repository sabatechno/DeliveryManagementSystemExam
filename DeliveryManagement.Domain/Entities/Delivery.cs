using DeliveryManagement.Domain.Common;
using DeliveryManagement.Domain.Enums;

namespace DeliveryManagement.Domain.Entities;

public class Delivery : BaseEntity
{
    public string DeliveryNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int ZoneId { get; set; }
    public Zone Zone { get; set; } = null!;
    public int? CourierId { get; set; }
    public Courier? Courier { get; set; }
    public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;
    public decimal Fee { get; set; }
    public DateTime? AssignedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string? FailureReason { get; set; }
    public ICollection<Package> Packages { get; set; } = new List<Package>();
}