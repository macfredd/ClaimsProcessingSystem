using Claims.Application.Claims;
using Claims.Application.WorksOrders;
using Microsoft.Extensions.DependencyInjection;

namespace Claims.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register handlers for claims
        services.AddScoped<SubmitClaimHandler>();
        services.AddScoped<GetClaimByIdHandler>();

        // Register handlers for work orders
        services.AddScoped<CreateWorkOrderHandler>();
        services.AddScoped<GetWorkOrderByIdHandler>();

        return services;
    }

}
