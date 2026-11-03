namespace DeliveryManagement.Application.Common.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
    string? IpAddress { get; }
    bool IsInRole(string role);
}