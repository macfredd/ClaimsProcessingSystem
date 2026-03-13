using Claims.Application.WorksOrders;
using Claims.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkOrdersController : ControllerBase
{
    private readonly CreateWorkOrderHandler _createWorkOrderHandler;
    private readonly GetWorkOrderByIdHandler _getWorkOrderByIdHandler;

    public WorkOrdersController(CreateWorkOrderHandler createWorkOrderHandler, GetWorkOrderByIdHandler getWorkOrderByIdHandler)
    {
        _createWorkOrderHandler = createWorkOrderHandler;
        _getWorkOrderByIdHandler = getWorkOrderByIdHandler;
    }

    [HttpPost]
    [ProducesResponseType(typeof(WorkOrderResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateWorkOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateWorkOrderCommand(
            request.ClaimId, 
            request.Type,
            request.AssignedTo);

        var workOrder = await _createWorkOrderHandler.HandleAsync(command, cancellationToken);

        return Created($"/api/WorksOrders/{workOrder.Id}", new WorkOrderResponse(
            workOrder.Id,
            workOrder.ClaimId,
            workOrder.Type,
            workOrder.Status,
            workOrder.AssignedTo,
            workOrder.CreatedAt
        ));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WorkOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWorkOrderById(Guid id,
        CancellationToken cancellationToken = default)
    {
        var workOrder = await _getWorkOrderByIdHandler.HandleAsync(id, cancellationToken);
        if (workOrder == null)
        {
            return NotFound();
        }
        return Ok(new WorkOrderResponse(
            workOrder.Id,
            workOrder.ClaimId,
            workOrder.Type,
            workOrder.Status,
            workOrder.AssignedTo,
            workOrder.CreatedAt
        ));
    }
}

public record CreateWorkOrderRequest(
    Guid ClaimId,
    WorkOrderType Type,
    string? AssignedTo
);
public record WorkOrderResponse(
    Guid Id,
    Guid ClaimId,
    WorkOrderType Type,
    WorkOrderStatus Status,
    string? AssignedTo,
    DateTime CreatedAt
);
