using Claims.Domain.Events;

namespace Claims.Application.Events;

/// <summary>
/// Event bus for publishing and subscribing to domain events.
/// </summary>
public interface IEventBus : IEventPublisher
{
    /// <summary>
    /// Subscribes a handler to receive events of type T.
    /// </summary>
    void Subscribe<T>(Func<T, CancellationToken, Task> handler) where T : DomainEvent;
}
