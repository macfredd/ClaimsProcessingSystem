using Claims.Domain.Enums;

namespace Claims.Domain.Entities;

/// <summary>
/// Work order generated when a claim is approved or requires action.
/// </summary>
public class WorkOrder
{
    public Guid Id { get; set; }
    public Guid ClaimId { get; set; }
    public WorkOrderType Type { get; set; }
    public WorkOrderStatus Status { get; set; }
    public string? AssignedTo { get; set; }
    public DateTime CreatedAt { get; set; }
}
