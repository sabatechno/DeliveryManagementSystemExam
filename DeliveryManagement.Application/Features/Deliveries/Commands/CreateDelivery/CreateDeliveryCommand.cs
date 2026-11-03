using DeliveryManagement.Domain.Enums;
using MediatR;

namespace DeliveryManagement.Application.Features.Deliveries.Commands.CreateDelivery;

public record CreateDeliveryCommand(
    int CustomerId,
    int ZoneId,
    List<PackageDto> Packages
) : IRequest<int>;

public record PackageDto(string Description, decimal Weight);