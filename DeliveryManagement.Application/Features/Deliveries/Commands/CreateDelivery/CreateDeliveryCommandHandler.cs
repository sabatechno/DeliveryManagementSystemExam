using DeliveryManagement.Application.Common.Interfaces;
using DeliveryManagement.Domain.Entities;
using DeliveryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeliveryManagement.Application.Features.Deliveries.Commands.CreateDelivery;

public class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateDeliveryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId, cancellationToken)
            ?? throw new KeyNotFoundException("Customer not found");

        var zone = await _context.Zones
            .FirstOrDefaultAsync(z => z.Id == request.ZoneId, cancellationToken)
            ?? throw new KeyNotFoundException("Zone not found");

        var delivery = new Delivery
        {
            DeliveryNumber = $"DEL-{DateTime.UtcNow:yyyyMMddHHmmss}",
            CustomerId = request.CustomerId,
            ZoneId = request.ZoneId,
            Status = DeliveryStatus.Pending,
            Fee = zone.BaseFee,
            Packages = request.Packages.Select(p => new Package
            {
                Description = p.Description,
                Weight = p.Weight
            }).ToList()
        };

        _context.Deliveries.Add(delivery);
        await _context.SaveChangesAsync(cancellationToken);

        return delivery.Id;
    }
}