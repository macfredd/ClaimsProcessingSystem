using Claims.Domain.Entities;

namespace Claims.RulesEngine;

/// <summary>
/// Service that evaluates a claim against business rules and returns a decision.
/// </summary>
public interface IClaimRulesEngine
{
    /// <summary>
    /// Evaluates the claim and returns the decision based on configured rules.
    /// </summary>
    ClaimDecision Evaluate(Claim claim);
}
