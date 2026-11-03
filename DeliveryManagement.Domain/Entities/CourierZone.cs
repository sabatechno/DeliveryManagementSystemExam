namespace DeliveryManagement.Domain.Entities;

public class CourierZone
{
    public int CourierId { get; set; }
    public Courier Courier { get; set; } = null!;
    public int ZoneId { get; set; }
    public Zone Zone { get; set; } = null!;
}