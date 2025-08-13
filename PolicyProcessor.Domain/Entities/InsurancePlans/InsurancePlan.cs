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
        Ulid policyTemplateFileId) : base(id)
    {
        Name = name;
        Price = price;
        PriceReasoning = priceReasoning;
        LifetimeInDays = lifetimeInDays;
        PolicyTemplateFileId = policyTemplateFileId;
    }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string PriceReasoning { get; set; }

    public int LifetimeInDays { get; set; }

    public Ulid PolicyTemplateFileId { get; set; }
}