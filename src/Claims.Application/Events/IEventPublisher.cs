using Claims.Domain.Events;

namespace Claims.Application.Events;

/// <summary>
/// Publishes domain events for asynchronous processing.
/// </summary>
public interface IEventPublisher
{
    Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default);
}
