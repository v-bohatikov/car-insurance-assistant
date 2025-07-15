namespace PolicyProcessor.Infrastructure.Contracts.Models;

public record InsurancePlanDto(
    long Id,
    string Name,
    decimal Price,
    string PriceReasoning,
    int LifetimeInDays,
    Guid PolicyTemplateFileId);