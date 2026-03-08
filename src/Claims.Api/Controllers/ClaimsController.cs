using Claims.Application.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClaimsController : ControllerBase
{
    private readonly SubmitClaimHandler _submitClaimHandler;

    public ClaimsController(SubmitClaimHandler submitClaimHandler)
    {
        _submitClaimHandler = submitClaimHandler;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClaimResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] SubmitClaimRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new SubmitClaimCommand(
            request.CustomerId,
            request.Type,
            request.Amount,
            request.Description);

        var claim = await _submitClaimHandler.HandleAsync(command, cancellationToken);

        return Created($"/api/claims/{claim.Id}", new ClaimResponse(
            claim.Id,
            claim.CustomerId,
            claim.Type,
            claim.Amount,
            claim.Description,
            claim.Status.ToString(),
            claim.CreatedAt,
            claim.UpdatedAt));
    }
}

public record SubmitClaimRequest(string CustomerId, string Type, decimal Amount, string? Description);

public record ClaimResponse(
    Guid Id,
    string CustomerId,
    string Type,
    decimal Amount,
    string? Description,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt);
