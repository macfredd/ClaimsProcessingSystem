using Claims.Domain.Entities;

namespace Claims.Application.Repositories;

/// <summary>
/// Repository for Claim persistence operations.
/// </summary>
public interface IClaimRepository
{
    Task<Claim> AddAsync(Claim claim, CancellationToken cancellationToken = default);

    Task UpdateAsync(Claim claim, CancellationToken cancellationToken = default);
    Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
