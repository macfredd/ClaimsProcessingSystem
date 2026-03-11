using Claims.Application.Repositories;
using Claims.Domain.Entities;

namespace Claims.Application.WorksOrders;

public class GetWorkOrderByIdHandler
{
    private readonly IWorkOrderRepository workOrderRepository;

    public GetWorkOrderByIdHandler(IWorkOrderRepository worksOrdersRepository)
    {
        this.workOrderRepository = worksOrdersRepository;
    }

    public async Task<WorkOrder?> HandleAsync(Guid id, CancellationToken ct)
    {
        return await workOrderRepository.GetByIdAsync(id, ct);
    }
}