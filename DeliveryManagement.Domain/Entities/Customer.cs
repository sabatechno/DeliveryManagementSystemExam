using DeliveryManagement.Domain.Common;

namespace DeliveryManagement.Domain.Entities;

public class Customer : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int ZoneId { get; set; }
    public Zone Zone { get; set; } = null!;
}