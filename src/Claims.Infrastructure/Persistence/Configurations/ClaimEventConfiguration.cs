using Claims.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Claims.Infrastructure.Persistence.Configurations;

internal class ClaimEventConfiguration : IEntityTypeConfiguration<ClaimEventEntity>
{
    public void Configure(EntityTypeBuilder<ClaimEventEntity> builder)
    {
        builder.ToTable("claim_events");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ClaimId)
            .IsRequired();

        builder.Property(e => e.EventType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Payload)
            .HasMaxLength(4000);

        builder.Property(e => e.OccurredAt)
            .IsRequired();

        builder.HasIndex(e => e.ClaimId);
        builder.HasIndex(e => e.OccurredAt);
    }
}
