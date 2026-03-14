using Claims.Domain.Entities;
using Claims.Domain.Enums;

namespace Claims.RulesEngine;

/// <summary>
/// Default implementation of the rules engine.
/// Evaluates claims based on amount thresholds.
/// </summary>
public class ClaimRulesEngine : IClaimRulesEngine
{
    private const decimal AutoApproveThreshold = 500m;
    private const decimal EscalateThreshold = 5000m;

    public ClaimDecision Evaluate(Claim claim)
    {
        var (decision, reason) = EvaluateRules(claim.Amount);

        return new ClaimDecision
        {
            Id = Guid.NewGuid(),
            ClaimId = claim.Id,
            Decision = decision,
            Reason = reason,
            CreatedAt = DateTime.UtcNow
        };
    }

    private static (ClaimDecisionType Decision, string Reason) EvaluateRules(decimal amount)
    {
        if (amount < AutoApproveThreshold)
        {
            return (ClaimDecisionType.Approve, "Auto-approved: amount below threshold");
        }

        if (amount > EscalateThreshold)
        {
            return (ClaimDecisionType.Escalate, "Escalated: amount exceeds limit");
        }

        return (ClaimDecisionType.Escalate, "Manual review required: amount in medium range");
    }
}
