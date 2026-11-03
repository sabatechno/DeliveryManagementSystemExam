using DeliveryManagement.Application.Features.Deliveries.Commands.CreateDelivery;
using DeliveryManagement.Application.Features.Deliveries.Queries.GetDeliveries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DeliveriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeliveriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<DeliveryDto>>> GetDeliveries(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] int? zoneId = null,
        [FromQuery] int? courierId = null,
        [FromQuery] DeliveryStatus? status = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetDeliveriesQuery(pageNumber, pageSize, fromDate, toDate, zoneId, courierId, status);
        return Ok(await _mediator.Send(query, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateDelivery(
        [FromBody] CreateDeliveryCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetDeliveries), new { id }, id);
    }
}