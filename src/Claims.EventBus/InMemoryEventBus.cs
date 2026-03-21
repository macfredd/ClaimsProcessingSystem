using System.Collections.Concurrent;
using Claims.Application.Events;
using Claims.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Claims.EventBus;

/// <summary>
/// In-memory implementation of the event bus for development.
/// Events are published synchronously to registered handlers.
/// </summary>
public class InMemoryEventBus : IEventBus
{
    private readonly ILogger<InMemoryEventBus> _logger;
    private readonly ConcurrentDictionary<Type, List<Func<DomainEvent, CancellationToken, Task>>> _handlers = new();

    public InMemoryEventBus(ILogger<InMemoryEventBus> logger)
    {
        _logger = logger;
    }

    public void Subscribe<T>(Func<T, CancellationToken, Task> handler) where T : DomainEvent
    {
        var eventType = typeof(T);
        var wrapper = (DomainEvent evt, CancellationToken ct) =>
        {
            if (evt is T typedEvent)
            {
                return handler(typedEvent, ct);
            }
            return Task.CompletedTask;
        };

        _handlers.AddOrUpdate(
            eventType,
            _ => [wrapper],
            (_, list) =>
            {
                lock (list) list.Add(wrapper);
                return list;
            });
    }

    public async Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var eventType = domainEvent.GetType();

        _logger.LogInformation(
            "Event published: {EventType} (Id: {EventId})",
            eventType.Name,
            domainEvent.EventId);

        if (_handlers.TryGetValue(eventType, out var handlers))
        {
            List<Func<DomainEvent, CancellationToken, Task>> snapshot;
            lock (handlers)
            {
                snapshot = [.. handlers];
            }

            foreach (var handler in snapshot)
            {
                await handler(domainEvent, cancellationToken);
            }
        }
    }
}
