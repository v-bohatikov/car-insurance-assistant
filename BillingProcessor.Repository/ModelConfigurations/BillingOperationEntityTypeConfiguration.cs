using BillingProcessor.Domain.Entities.BillingOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Infrastructure.EntityTypeConfigurators;
using SharedKernel.Enums;

namespace BillingProcessor.Repository.ModelConfigurations;

public class BillingOperationEntityTypeConfiguration
    : EntityTypeConfigurationBase<BillingOperation>
{
    protected override void ConfigureInner(EntityTypeBuilder<BillingOperation> builder)
    {
        // Table name.
        builder.ToTable("BillingOperations");

        // Primary key.
        builder.HasKey(billingOperation => billingOperation.Id);

        builder
            .Property(billingOperation => billingOperation.Id)
            // According to Ulid specification.
            .HasMaxLength(26)
            .ValueGeneratedNever();

        // Relationships.
        // No relationships here.

        // Properties.
        builder
            .Property(billingOperation => billingOperation.OrderId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(billingOperation => billingOperation.OperationStatus)
            .HasConversion<EnumToStringConverter<BillingOperationStatus>>()
            // Should be more than enough for enum field.
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(billingOperation => billingOperation.FailureReasoning)
            // Should be more than enough.
            .HasMaxLength(300)
            .IsRequired(false);

        // Indexes.
        builder
            .HasIndex(billingOperation => billingOperation.OrderId)
            .IsUnique();

        // Seeding.
        // No seeding required.
    }
}