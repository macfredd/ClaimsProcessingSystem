using Claims.Application.Repositories;
using Claims.Domain.Entities;
using Claims.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Claims.Infrastructure.Repositories;

/// <summary>
/// EF Core implementation of the claim repository.
/// </summary>
internal class ClaimRepository : IClaimRepository
{
    private readonly ClaimsDbContext _context;

    public ClaimRepository(ClaimsDbContext context)
    {
        _context = context;
    }

    public async Task<Claim> AddAsync(Claim claim, CancellationToken cancellationToken = default)
    {
        _context.Claims.Add(claim);
        await _context.SaveChangesAsync(cancellationToken);
        return claim;
    }

    public async Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Claims.FindAsync([id], cancellationToken);
    }
}
