using Claims.Domain.Entities;

namespace Claims.Application.Repositories;

/// <summary>
/// Defines a contract for managing and retrieving work orders in a data store.
/// </summary>
/// <remarks>This interface provides asynchronous methods for adding new work orders and retrieving existing work
/// orders by their unique identifier or associated claim. Implementations are expected to handle data persistence and
/// retrieval, and should support cancellation via the provided cancellation tokens.</remarks>
public interface IWorkOrderRepository
{
    Task<WorkOrder> AddAsync(WorkOrder workOrder, CancellationToken cancellationToken = default);
    Task<WorkOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<WorkOrder>> GetByClaimIdAsync(Guid claimId, CancellationToken cancellationToken = default);
}
