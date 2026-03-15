using Claims.Domain.Enums;

namespace Claims.Application.Claims;

/// <summary>
/// Command to submit a new claim.
/// </summary>
public record SubmitClaimCommand(
    string CustomerId,
    ClaimType Type,
    decimal Amount,
    string? Description
);
