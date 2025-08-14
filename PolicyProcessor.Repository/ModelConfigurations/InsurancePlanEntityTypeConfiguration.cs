using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolicyProcessor.Domain.Entities.InsurancePlans;

namespace PolicyProcessor.Repository.ModelConfigurations;

public class InsurancePlanEntityTypeConfiguration : IEntityTypeConfiguration<InsurancePlan>
{
    public void Configure(EntityTypeBuilder<InsurancePlan> builder)
    {
        // Table name.
        builder.ToTable("InsurancePlans");

        // Primary key.
        builder.HasKey(insurancePlan => insurancePlan.Id);

        builder
            .Property(insurancePlan => insurancePlan.Id)
            // According to Ulid specification.
            .HasMaxLength(26)
            .ValueGeneratedNever();

        // Relationships.
        // Because the only relation is with 'InsurancePolicy' entity we don't need to duplicate it here.

        // Properties.
        builder
            .Property(insurancePlan => insurancePlan.Name)
            // Should be more than enough.
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(insurancePlan => insurancePlan.Price)
            // Should be more than enough.
            .HasPrecision(7, 2)
            .IsRequired();

        builder
            .Property(insurancePlan => insurancePlan.PriceReasoning)
            // Should be more than enough.
            .HasMaxLength(300)
            .IsRequired();

        builder
            .Property(insurancePlan => insurancePlan.LifetimeInDays)
            .IsRequired();

        builder
            .Property(insurancePlan => insurancePlan.PolicyTemplateDocumentId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();
    }
}