using SharedKernel;

namespace PolicyProcessor.Domain.Entities.InsurancePlans;

public sealed class InsurancePlan : Entity
{
    private InsurancePlan(
        long id,
        string name,
        decimal price,
        string priceReasoning,
        int lifetimeInDays,
        Guid policyTemplate) : base(id)
    {
        Name = name;
        Price = price;
        PriceReasoning = priceReasoning;
        LifetimeInDays = lifetimeInDays;
        PolicyTemplate = policyTemplate;
    }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string PriceReasoning { get; set; }

    public int LifetimeInDays { get; set; }

    public Guid PolicyTemplate { get; set; }
}