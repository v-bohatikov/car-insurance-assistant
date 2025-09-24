using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolicyProcessor.Domain.Entities.InsurancePlans;
using Repository.Infrastructure.EntityTypeConfigurators;

namespace PolicyProcessor.Repository.ModelConfigurations;

public class InsurancePlanEntityTypeConfiguration
    : EntityTypeConfigurationBase<InsurancePlan>
{
    protected override void ConfigureInner(EntityTypeBuilder<InsurancePlan> builder)
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

        // Indexes.
        // No indexes required.

        // Seeding.
        builder.HasData(GetSeedingData());
    }

    private static ICollection<InsurancePlan> GetSeedingData()
    {
        const string basicPlanId = "01K33RQRZ3MA3CJFR6JMBM8HXF";
        // This id was gotten from 'DocumentEntityTypeConfiguration'.
        const string basicPolicyTemplateDocumentId = "01K33RW7YFCVHMWJM3BT9VSHF8";
        const string createdOn = "2025-08-21 15:53:44";
        var basicInsurancePlan = new InsurancePlan(
            Ulid.Parse(basicPlanId),
            "Basic Plan",
            25,
            "This plan provides basic level of protection and services. " +
            "NOTE: Price is fixed for all clients for this insurance plan.",
            7,
            Ulid.Parse(basicPolicyTemplateDocumentId));
        basicInsurancePlan.CreatedOn = DateTime.Parse(createdOn);

        const string premiumPlanId = "01K33RT9B4R5H40M0S501EWAYN";
        // This id was gotten from 'DocumentEntityTypeConfiguration'.
        const string premiumPolicyTemplateDocumentId = "01K33RWMYKQTSCGBXMWZTQCCKK";
        var premiumInsurancePlan = new InsurancePlan(
            Ulid.Parse(premiumPlanId),
            "Premium Plan",
            100,
            "This plan provides premium level of protection and services. " +
            "NOTE: Price is entity fixed for all clients for this insurance plan.",
            7,
            Ulid.Parse(premiumPolicyTemplateDocumentId));
        premiumInsurancePlan.CreatedOn = DateTime.Parse(createdOn);

        var insurancePlans = new[]
        {
            basicInsurancePlan,
            premiumInsurancePlan,
        };

        return insurancePlans;
    }
}