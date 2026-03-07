using Claims.Domain.Enums;

namespace Claims.Domain.Events;

/// <summary>
/// Event raised when a work order is created.
/// </summary>
public class WorkOrderCreated : DomainEvent
{
    public Guid WorkOrderId { get; init; }
    public Guid ClaimId { get; init; }
    public WorkOrderType Type { get; init; }
}
