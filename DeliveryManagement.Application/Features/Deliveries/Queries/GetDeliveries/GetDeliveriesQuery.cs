using DeliveryManagement.Domain.Enums;
using MediatR;

namespace DeliveryManagement.Application.Features.Deliveries.Queries.GetDeliveries;

public record GetDeliveriesQuery(
    int PageNumber = 1,
    int PageSize = 10,
    DateTime? FromDate = null,
    DateTime? ToDate = null,
    int? ZoneId = null,
    int? CourierId = null,
    DeliveryStatus? Status = null
) : IRequest<PagedResult<DeliveryDto>>;

public record DeliveryDto(
    int Id,
    string DeliveryNumber,
    string CustomerName,
    string ZoneName,
    string? CourierName,
    string Status,
    decimal Fee,
    DateTime CreatedAt,
    int PackageCount
);

public record PagedResult<T>(List<T> Items, int TotalCount, int PageNumber, int PageSize);