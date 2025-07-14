namespace PolicyProcessor.Contracts.Models;

public record InsurancePlan(
    long Id,
    string Name,
    decimal Price,
    int LifetimeInDays,
    Guid PolicyTemplateFileId);