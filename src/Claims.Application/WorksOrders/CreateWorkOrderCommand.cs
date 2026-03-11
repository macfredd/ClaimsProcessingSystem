using Claims.Domain.Enums;

namespace Claims.Application.WorksOrders;

/// <summary>
/// Command to create a new work order for a claim.
/// </summary>
public record CreateWorkOrderCommand
(
    Guid ClaimId,
    WorkOrderType Type,
    string? AssignedTo
);
