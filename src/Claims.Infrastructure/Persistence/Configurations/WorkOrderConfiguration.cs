using Claims.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Claims.Infrastructure.Persistence.Configurations;

internal class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.ToTable("work_orders");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.ClaimId)
            .IsRequired();

        builder.Property(w => w.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(w => w.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(w => w.AssignedTo)
            .HasMaxLength(200);

        builder.Property(w => w.CreatedAt)
            .IsRequired();

        builder.HasIndex(w => w.ClaimId);
        builder.HasIndex(w => w.Status);
    }
}
