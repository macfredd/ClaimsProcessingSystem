using Claims.Application.Events;
using Claims.Application.Repositories;
using Claims.Domain.Entities;
using Claims.Domain.Enums;
using Claims.Domain.Events;

namespace Claims.Application.Claims;

/// <summary>
/// Handles the SubmitClaim use case.
/// </summary>
public class SubmitClaimHandler
{
    private readonly IClaimRepository _claimRepository;
    private readonly IEventPublisher _eventPublisher;

    public SubmitClaimHandler(IClaimRepository claimRepository, IEventPublisher eventPublisher)
    {
        _claimRepository = claimRepository;
        _eventPublisher = eventPublisher;
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

        return savedClaim;
    }
}
