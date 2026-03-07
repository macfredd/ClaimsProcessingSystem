using Claims.Domain.Enums;

namespace Claims.Domain.Entities;

/// <summary>
/// Represents a decision made on a claim.
/// </summary>
public class ClaimDecision
{
    public Guid Id { get; set; }
    public Guid ClaimId { get; set; }
    public ClaimDecisionType Decision { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; }
}
