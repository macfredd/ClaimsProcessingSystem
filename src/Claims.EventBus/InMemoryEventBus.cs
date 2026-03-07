using Claims.Application.Events;
using Claims.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Claims.EventBus;

/// <summary>
/// In-memory implementation of the event bus for development.
/// Events are published synchronously to registered handlers.
/// </summary>
public class InMemoryEventBus : IEventPublisher
{
    private readonly ILogger<InMemoryEventBus> _logger;
    private readonly List<Func<DomainEvent, CancellationToken, Task>> _handlers = [];

    public InMemoryEventBus(ILogger<InMemoryEventBus> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Event published: {EventType} (Id: {EventId})",
            domainEvent.GetType().Name,
            domainEvent.EventId);

        return Task.CompletedTask;
    }
}
