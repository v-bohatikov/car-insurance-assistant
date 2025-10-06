namespace ApiGateway.Contracts.Models;

public record InsurancePlan(
    Ulid Id,
    string Name,
    decimal Price,
    string PriceReasoning,
    int LifetimeInDays);