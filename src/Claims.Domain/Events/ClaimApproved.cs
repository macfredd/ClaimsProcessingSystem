namespace Claims.Domain.Events;

/// <summary>
/// Event raised when a claim is approved.
/// </summary>
public class ClaimApproved : DomainEvent
{
    public Guid ClaimId { get; init; }
    public string? Reason { get; init; }
}
