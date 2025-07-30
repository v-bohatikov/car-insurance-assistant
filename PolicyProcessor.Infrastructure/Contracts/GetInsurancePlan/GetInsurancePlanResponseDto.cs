namespace PolicyProcessor.Infrastructure.Contracts.GetInsurancePlan;

public record GetInsurancePlanResponseDto(
    long Id,
    string Name,
    decimal Price,
    string PriceReasoning,
    int LifetimeInDays,
    Guid PolicyTemplateFileId);