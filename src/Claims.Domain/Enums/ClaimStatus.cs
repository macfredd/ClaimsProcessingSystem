namespace Claims.Domain.Enums;

/// <summary>
/// Possible states of a claim in the system.
/// </summary>
public enum ClaimStatus
{
    Submitted,
    UnderReview,
    AwaitingDocuments,
    Approved,
    Rejected,
    Escalated,
    Closed
}
