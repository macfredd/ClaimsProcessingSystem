using Claims.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Claims.Infrastructure.Persistence.Configurations;

internal class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.ToTable("claims");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.CustomerId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Type)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(2000);

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .IsRequired();

        builder.HasIndex(c => c.CustomerId);
        builder.HasIndex(c => c.Status);
    }
}
