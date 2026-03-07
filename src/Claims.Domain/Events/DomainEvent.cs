namespace Claims.Domain.Events;

/// <summary>
/// Base class for all domain events in the system.
/// </summary>
public abstract class DomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
