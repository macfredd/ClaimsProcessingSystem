namespace Claims.Domain.Events;

/// <summary>
/// Event raised when a claim is received and persisted.
/// </summary>
public class ClaimSubmitted : DomainEvent
{
    public Guid ClaimId { get; init; }
    public required string CustomerId { get; init; }
    public required string Type { get; init; }
    public decimal Amount { get; init; }
}
