using Claims.Application.Repositories;
using Claims.Domain.Entities;
using Claims.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Repositories;

/// <summary>
/// EF Core implementation of the claim decision repository.
/// </summary>
internal class ClaimDecisionRepository : IClaimDecisionRepository
{
    private readonly ClaimsDbContext _context;

    public ClaimDecisionRepository(ClaimsDbContext context)
    {
        _context = context;
    }

    public async Task<ClaimDecision> AddAsync(ClaimDecision claimDecision, CancellationToken cancellationToken = default)
    {
        _context.ClaimDecisions.Add(claimDecision);
        await _context.SaveChangesAsync(cancellationToken);
        return claimDecision;
    }

    public async Task<ClaimDecision?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ClaimDecisions.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<ClaimDecision>> GetByClaimIdAsync(Guid claimId, CancellationToken cancellationToken = default)
    {
        return await _context.ClaimDecisions
            .Where(cd => cd.ClaimId == claimId)
            .OrderByDescending(cd => cd.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
