using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Claims.Infrastructure.Persistence;

/// <summary>
/// Design-time factory for EF Core migrations.
/// Enables running migrations without starting the application.
/// </summary>
public class ClaimsDbContextFactory : IDesignTimeDbContextFactory<ClaimsDbContext>
{
    public ClaimsDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Claims.Api");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=ClaimsDb;Username=postgres;Password=postgres";

        var optionsBuilder = new DbContextOptionsBuilder<ClaimsDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new ClaimsDbContext(optionsBuilder.Options);
    }
}
