namespace PolicyProcessor.Infrastructure.Contracts.GetInsurancePlan;

public record GetInsurancePlanResponseDto(
    Ulid Id,
    string Name,
    decimal Price,
    string PriceReasoning,
    int LifetimeInDays,
    Ulid PolicyTemplateDocumentId);