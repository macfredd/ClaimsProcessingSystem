using Claims.Application.Repositories;
using Claims.Domain.Entities;
using Claims.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Repositories;

/// <summary>
/// Provides data access methods for managing work orders in the database.
/// </summary>
/// <remarks>This class implements the IWorksOrdersRepository interface and is intended for internal use within
/// the data access layer. It encapsulates operations for adding and retrieving work orders associated with
/// claims.</remarks>
internal class WorkOrdersRepositoryRepository : IWorkOrderRepository
{

    private readonly ClaimsDbContext _context;

    public WorkOrdersRepositoryRepository(ClaimsDbContext context)
    {
        _context = context;
    }

    public async Task<WorkOrder> AddAsync(WorkOrder workOrder, CancellationToken cancellationToken = default)
    {
        _context.WorkOrders.Add(workOrder);
        await _context.SaveChangesAsync(cancellationToken);
        return workOrder;
    }

    public async Task<IEnumerable<WorkOrder>> GetByClaimIdAsync(Guid claimId, CancellationToken cancellationToken = default)
    {
        return await _context.WorkOrders.Where(wo => wo.ClaimId == claimId).ToListAsync(cancellationToken);
    }

    public async Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.WorkOrders.FindAsync([id], cancellationToken);
    }
}
