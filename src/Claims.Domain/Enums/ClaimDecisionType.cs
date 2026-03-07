namespace Claims.Domain.Enums;

/// <summary>
/// Types of decision the system can make on a claim.
/// </summary>
public enum ClaimDecisionType
{
    Approve,
    Reject,
    RequestDocuments,
    Escalate
}
