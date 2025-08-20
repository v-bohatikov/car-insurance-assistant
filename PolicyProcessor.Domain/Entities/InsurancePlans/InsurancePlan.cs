using SharedKernel;

namespace PolicyProcessor.Domain.Entities.InsurancePlans;

public sealed class InsurancePlan(
    Ulid id,
    string name,
    decimal price,
    string priceReasoning,
    int lifetimeInDays,
    Ulid policyTemplateDocumentId)
    : Entity(id)
{
    public string Name { get; } = name;

    public decimal Price { get; } = price;

    public string PriceReasoning { get; } = priceReasoning;

    public int LifetimeInDays { get; } = lifetimeInDays;

    public Ulid PolicyTemplateDocumentId { get; } = policyTemplateDocumentId;
}