namespace Claims.Infrastructure.Persistence.Entities;

/// <summary>
/// Persistence entity for storing domain events for audit trail.
/// </summary>
public class ClaimEventEntity
{
    public Guid Id { get; set; }
    public Guid ClaimId { get; set; }
    public required string EventType { get; set; }
    public string? Payload { get; set; }
    public DateTime OccurredAt { get; set; }
}
