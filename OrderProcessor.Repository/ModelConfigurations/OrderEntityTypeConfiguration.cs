using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderProcessor.Domain.Entities.Orders;
using Repository.Infrastructure.EntityTypeConfigurators;

namespace OrderProcessor.Repository.ModelConfigurations;

public class OrderEntityTypeConfiguration
    : EntityTypeConfigurationBase<Order>
{
    protected override void ConfigureInner(EntityTypeBuilder<Order> builder)
    {
        // Table name.
        builder.ToTable("Orders");

        // Primary key.
        builder.HasKey(insurancePlan => insurancePlan.Id);

        builder
            .Property(insurancePlan => insurancePlan.Id)
            // According to Ulid specification.
            .HasMaxLength(26)
            .ValueGeneratedNever();

        // Relationships.
        // No relationships here.

        // Properties.
        builder
            .Property(order => order.UserId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(order => order.VehicleId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(order => order.InsurancePlanId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(order => order.FailureReasoning)
            // Should be more than enough.
            .HasMaxLength(300)
            .IsRequired(false);

        // Indexes.
        builder
            .HasIndex(order => new { order.UserId, order.VehicleId })
            .IsUnique(false);

        builder
            .HasIndex(order => new { order.UserId, order.VehicleId, order.InsurancePlanId })
            .IsUnique(false);

        // Seeding.
        // No seeding required.
    }
}