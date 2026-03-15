using Claims.Domain.Enums;

namespace Claims.Domain.Entities;


/// <summary>
/// Main entity representing a claim in the system.
/// </summary>
public class Claim
{
    public Guid Id { get; set; }
    public required string CustomerId { get; set; }
    public ClaimType Type { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public ClaimStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
