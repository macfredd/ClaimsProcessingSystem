using Microsoft.Extensions.DependencyInjection;

namespace Claims.RulesEngine;

/// <summary>
/// Extension methods for registering Rules Engine services.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddRulesEngine(this IServiceCollection services)
    {
        services.AddScoped<IClaimRulesEngine, ClaimRulesEngine>();
        return services;
    }
}
