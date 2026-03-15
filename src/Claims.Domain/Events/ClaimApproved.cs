using Claims.Domain.Enums;

namespace Claims.Domain.Events;

/// <summary>
/// Event raised when a claim is approved by the rules engine.
/// </summary>
public class ClaimApproved : DomainEvent
{
    public Guid ClaimId { get; init; }
    public required string CustomerId { get; init; }
    public ClaimType Type { get; init; }
    public decimal Amount { get; init; }
    public Guid WorkOrderId { get; init; }
}
