using Claims.Application.Events;
using Claims.Application.Repositories;
using Claims.Domain.Entities;
using Claims.Domain.Enums;
using Claims.Domain.Events;

namespace Claims.Application.WorksOrders;

public class CreateWorkOrderHandler
{
    private readonly IWorkOrderRepository _workOrderRepository;
    private readonly IEventPublisher _eventPublisher;

    public CreateWorkOrderHandler(IWorkOrderRepository workOrderRepository, IEventPublisher eventPublisher)
    {
        _workOrderRepository = workOrderRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<WorkOrder> HandleAsync(CreateWorkOrderCommand createWorkOrderCommand, CancellationToken cancellationToken = default)
    {
        var workOrder = new WorkOrder
        {
            Id = Guid.NewGuid(),
            ClaimId = createWorkOrderCommand.ClaimId,
            Type = createWorkOrderCommand.Type,
            AssignedTo = createWorkOrderCommand.AssignedTo,
            Status = WorkOrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        var savedWorkOrder = await _workOrderRepository.AddAsync(workOrder, cancellationToken);

        await _eventPublisher.PublishAsync(new WorkOrderCreated
        {
            WorkOrderId = savedWorkOrder.Id,
            ClaimId = savedWorkOrder.ClaimId,
            Type = savedWorkOrder.Type
        }, cancellationToken);

        return savedWorkOrder;
    }

}
