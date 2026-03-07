namespace Claims.Application.Claims;

/// <summary>
/// Command to submit a new claim.
/// </summary>
public record SubmitClaimCommand(
    string CustomerId,
    string Type,
    decimal Amount,
    string? Description
);
