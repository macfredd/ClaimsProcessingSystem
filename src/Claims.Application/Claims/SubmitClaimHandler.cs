using Claims.Application.Events;
using Claims.Application.Repositories;
using Claims.Domain.Entities;
using Claims.Domain.Enums;
using Claims.Domain.Events;
using Claims.RulesEngine;

namespace Claims.Application.Claims;

/// <summary>
/// Handles the SubmitClaim use case.
/// </summary>
public class SubmitClaimHandler
{
    private readonly IClaimRepository _claimRepository;
    private readonly IClaimDecisionRepository _claimDecisionRepository;
    private readonly IWorkOrderRepository _workOrderRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IClaimRulesEngine _claimRuleEngine;

    public SubmitClaimHandler(
        IClaimRepository claimRepository, 
        IClaimDecisionRepository claimDecisionRepository,
        IWorkOrderRepository workOrderRepository,
        IEventPublisher eventPublisher, 
        IClaimRulesEngine claimRulesEngine)
    {
        _claimRepository = claimRepository;
        _claimDecisionRepository = claimDecisionRepository;
        _workOrderRepository = workOrderRepository;
        _eventPublisher = eventPublisher;
        _claimRuleEngine = claimRulesEngine;
    }

    public async Task<Claim> HandleAsync(SubmitClaimCommand command, CancellationToken cancellationToken = default)
    {
        var claim = new Claim
        {
            Id = Guid.NewGuid(),
            CustomerId = command.CustomerId,
            Type = command.Type,
            Amount = command.Amount,
            Description = command.Description,
            Status = ClaimStatus.Submitted,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var savedClaim = await _claimRepository.AddAsync(claim, cancellationToken);

        await _eventPublisher.PublishAsync(new ClaimSubmitted
        {
            ClaimId = savedClaim.Id,
            CustomerId = savedClaim.CustomerId,
            Type = savedClaim.Type,
            Amount = savedClaim.Amount
        }, cancellationToken);

        var claimDecision = _claimRuleEngine.Evaluate(savedClaim);

        if (claimDecision != null)
        {
            await _claimDecisionRepository.AddAsync(claimDecision, cancellationToken);

            claim.Status = claimDecision.Decision switch
            {
                ClaimDecisionType.Approve => ClaimStatus.Approved,
                ClaimDecisionType.Reject => ClaimStatus.Rejected,
                ClaimDecisionType.Escalate => ClaimStatus.Escalated,
                _ => claim.Status
            };
            claim.UpdatedAt = DateTime.UtcNow;
            await _claimRepository.UpdateAsync(claim, cancellationToken);

            if (claim.Status == ClaimStatus.Approved)
            {
                var newWorkOrder = new WorkOrder
                {
                    Id = Guid.NewGuid(),
                    ClaimId = claim.Id,
                    Status = WorkOrderStatus.Pending,
                    AssignedTo = "SystemAdmin",
                    CreatedAt = DateTime.UtcNow
                };

                newWorkOrder.Type = claim.Type switch
                {
                    ClaimType.Refund => newWorkOrder.Type = WorkOrderType.IssueRefund,
                    ClaimType.Replacement => newWorkOrder.Type = WorkOrderType.ShipReplacement,
                    ClaimType.Inspection => newWorkOrder.Type = WorkOrderType.ScheduleInspection,
                    _ => WorkOrderType.ManualReview
                };

                var savedWorkOrder = await _workOrderRepository.AddAsync(newWorkOrder, cancellationToken);

                await _eventPublisher.PublishAsync(new WorkOrderCreated
                {
                    WorkOrderId = savedWorkOrder.Id,
                    ClaimId = claim.Id,
                    Type = savedWorkOrder.Type
                }, cancellationToken);

                await _eventPublisher.PublishAsync(new ClaimApproved
                {
                    ClaimId = claim.Id,
                    CustomerId = claim.CustomerId,
                    Type = claim.Type,
                    Amount = claim.Amount,
                    WorkOrderId = savedWorkOrder.Id
                }, cancellationToken);
            }
            else if (claim.Status == ClaimStatus.Rejected)
            {
                await _eventPublisher.PublishAsync(new ClaimRejected
                {
                    ClaimId = claim.Id,
                    CustomerId = claim.CustomerId,
                    Type = claim.Type,
                    Amount = claim.Amount,
                    Reason = claimDecision.Reason
                }, cancellationToken);
            }
        }

        return savedClaim;
    }
}
