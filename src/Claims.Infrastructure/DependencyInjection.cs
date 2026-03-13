using Claims.Application.Repositories;
using Claims.Infrastructure.Persistence;
using Claims.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Claims.Infrastructure;

/// <summary>
/// Extension methods for registering Infrastructure services.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ClaimsDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IClaimRepository, ClaimRepository>();
        services.AddScoped<IWorkOrderRepository, WorkOrdersRepositoryRepository>();

        return services;
    }
}
