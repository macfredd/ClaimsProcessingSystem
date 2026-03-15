using Claims.Domain.Enums;

namespace Claims.Domain.Events;

/// <summary>
/// Event raised when a claim is rejected by the rules engine.
/// </summary>
public class ClaimRejected : DomainEvent
{
    public Guid ClaimId { get; init; }
    public required string CustomerId { get; init; }
    public ClaimType Type { get; init; }
    public decimal Amount { get; init; }
    public string? Reason { get; init; }
}
