using Claims.Domain.Entities;
using Claims.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core database context for the Claims Processing System.
/// </summary>
public class ClaimsDbContext : DbContext
{
    public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Claim> Claims => Set<Claim>();
    public DbSet<ClaimDecision> ClaimDecisions => Set<ClaimDecision>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<ClaimEventEntity> ClaimEvents => Set<ClaimEventEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClaimsDbContext).Assembly);
    }
}
