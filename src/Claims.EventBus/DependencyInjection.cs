using Claims.Application.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Claims.EventBus;

/// <summary>
/// Extension methods for registering EventBus services.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        services.AddSingleton<IEventPublisher>(sp => sp.GetRequiredService<IEventBus>());
        return services;
    }
}
