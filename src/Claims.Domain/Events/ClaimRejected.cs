namespace Claims.Domain.Events;

/// <summary>
/// Event raised when a claim is rejected.
/// </summary>
public class ClaimRejected : DomainEvent
{
    public Guid ClaimId { get; init; }
    public required string Reason { get; init; }
}
