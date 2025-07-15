namespace PolicyProcessor.Contracts.Models;

public record InsurancePlan(
    long Id,
    string Name,
    decimal Price,
    string PriceReasoning,
    int LifetimeInDays,
    Guid PolicyTemplateFileId);