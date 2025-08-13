namespace PolicyProcessor.Infrastructure.Contracts.Models;

public record InsurancePlanDto(
    Ulid Id,
    string Name,
    decimal Price,
    string PriceReasoning,
    int LifetimeInDays,
    Ulid PolicyTemplateFileId);