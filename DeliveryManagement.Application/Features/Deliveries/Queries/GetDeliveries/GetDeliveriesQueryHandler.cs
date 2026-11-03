using DeliveryManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryManagement.Application.Features.Deliveries.Queries.GetDeliveries;

public class GetDeliveriesQueryHandler : IRequestHandler<GetDeliveriesQuery, PagedResult<DeliveryDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDeliveriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<DeliveryDto>> Handle(GetDeliveriesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Deliveries.AsNoTracking().AsQueryable();

        if (request.FromDate.HasValue)
            query = query.Where(d => d.CreatedAt >= request.FromDate.Value);
        if (request.ToDate.HasValue)
            query = query.Where(d => d.CreatedAt <= request.ToDate.Value);
        if (request.ZoneId.HasValue)
            query = query.Where(d => d.ZoneId == request.ZoneId.Value);
        if (request.CourierId.HasValue)
            query = query.Where(d => d.CourierId == request.CourierId.Value);
        if (request.Status.HasValue)
            query = query.Where(d => d.Status == request.Status.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(d => d.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(d => new DeliveryDto(
                d.Id,
                d.DeliveryNumber,
                d.Customer.FullName,
                d.Zone.Name,
                d.Courier != null ? d.Courier.FullName : null,
                d.Status.ToString(),
                d.Fee,
                d.CreatedAt,
                d.Packages.Count
            ))
            .ToListAsync(cancellationToken);

        return new PagedResult<DeliveryDto>(items, totalCount, request.PageNumber, request.PageSize);
    }
}