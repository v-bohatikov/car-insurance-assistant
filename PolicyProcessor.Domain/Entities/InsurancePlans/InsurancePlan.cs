using SharedKernel;

namespace PolicyProcessor.Domain.Entities.InsurancePlans;

public sealed class InsurancePlan : Entity
{
    private InsurancePlan(
        Ulid id,
        string name,
        decimal price,
        string priceReasoning,
        int lifetimeInDays,
        Ulid policyTemplateDocumentId)
        : base(id)
    {
        Name = name;
        Price = price;
        PriceReasoning = priceReasoning;
        LifetimeInDays = lifetimeInDays;
        PolicyTemplateDocumentId = policyTemplateDocumentId;
    }

    public string Name { get; }

    public decimal Price { get; }

    public string PriceReasoning { get; }

    public int LifetimeInDays { get; }

    public Ulid PolicyTemplateDocumentId { get; }
}