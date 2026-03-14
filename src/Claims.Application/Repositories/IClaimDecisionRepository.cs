using Claims.Domain.Entities;

namespace Claims.Application.Repositories;

/// <summary>
/// Repository for ClaimDecision persistence operations.
/// </summary>
public interface IClaimDecisionRepository
{
    Task<ClaimDecision> AddAsync(ClaimDecision claimDecision, CancellationToken cancellationToken = default);
    Task<ClaimDecision?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ClaimDecision>> GetByClaimIdAsync(Guid claimId, CancellationToken cancellationToken = default);
}
