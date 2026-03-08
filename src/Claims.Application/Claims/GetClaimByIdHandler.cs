using Claims.Application.Repositories;
using Claims.Domain.Entities;

namespace Claims.Application.Claims
{
    public class GetClaimByIdHandler
    {
        private readonly IClaimRepository _claimRepository;

        public GetClaimByIdHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<Claim?> HandleAsync(Guid id, CancellationToken ct)
        {
            return await _claimRepository.GetByIdAsync(id, ct);
        }
    }
}
