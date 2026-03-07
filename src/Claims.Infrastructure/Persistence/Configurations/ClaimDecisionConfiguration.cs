using Claims.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Claims.Infrastructure.Persistence.Configurations;

internal class ClaimDecisionConfiguration : IEntityTypeConfiguration<ClaimDecision>
{
    public void Configure(EntityTypeBuilder<ClaimDecision> builder)
    {
        builder.ToTable("claim_decisions");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ClaimId)
            .IsRequired();

        builder.Property(c => c.Decision)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(c => c.Reason)
            .HasMaxLength(1000);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasIndex(c => c.ClaimId);
    }
}
