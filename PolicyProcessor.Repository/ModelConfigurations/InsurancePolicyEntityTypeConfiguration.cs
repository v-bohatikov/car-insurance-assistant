using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolicyProcessor.Domain.Entities.InsurancePolicies;
using Repository.Infrastructure.EntityTypeConfigurators;
using SharedKernel.Enums;

namespace PolicyProcessor.Repository.ModelConfigurations;

public class InsurancePolicyEntityTypeConfiguration
    : EntityTypeConfigurationBase<InsurancePolicy>
{
    public override void ConfigureInner(EntityTypeBuilder<InsurancePolicy> builder)
    {
        // Table name.
        builder.ToTable("InsurancePolicies");

        // Primary key.
        builder.HasKey(insurancePolicy => insurancePolicy.Id);

        builder
            .Property(insurancePolicy => insurancePolicy.Id)
            // According to Ulid specification.
            .HasMaxLength(26)
            .ValueGeneratedNever();

        // Relationships.
        builder
            .HasOne(insurancePolicy => insurancePolicy.InsurancePlan)
            .WithMany()
            .HasForeignKey(insurancePolicy => insurancePolicy.InsurancePlanId)
            .IsRequired();
        builder
            .Navigation(insurancePolicy => insurancePolicy.InsurancePlan)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        // Properties.
        builder
            .Property(insurancePolicy => insurancePolicy.UserId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(insurancePolicy => insurancePolicy.VehicleId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(insurancePolicy => insurancePolicy.Status)
            .HasConversion<EnumToStringConverter<InsurancePolicyStatus>>()
            // Should be more than enough for enum field.
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(insurancePolicy => insurancePolicy.InsurancePlanId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(insurancePolicy => insurancePolicy.PolicyDocumentId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired(false);

        builder
            .Property(insurancePolicy => insurancePolicy.IssuedOn)
            .HasConversion<DateOnlyToStringConverter>()
            // According to converter hints.
            .HasMaxLength(10)
            .IsRequired(false);

        builder
            .Property(insurancePolicy => insurancePolicy.ExpiredAt)
            .HasConversion<DateOnlyToStringConverter>()
            // According to converter hints.
            .HasMaxLength(10)
            .IsRequired(false);

        builder
            .Property(insurancePolicy => insurancePolicy.FailureReasoning)
            // Should be more than enough.
            .HasMaxLength(300)
            .IsRequired(false);

        // Indexes.
        builder
            .HasIndex(insurancePolicy =>
                new { insurancePolicy.UserId, insurancePolicy.VehicleId })
            .IsUnique(false);

        builder
            .HasIndex(insurancePolicy =>
                new { insurancePolicy.UserId, insurancePolicy.VehicleId, insurancePolicy.InsurancePlanId })
            .IsUnique(false);

        // Seeding.
        // No seeding required.
    }
}