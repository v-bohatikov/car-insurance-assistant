namespace ApiGateway.Infrastructure.Contracts.GetInsurancePlan;

public record GetInsurancePlanResponseDto(
    Ulid Id,
    string Name,
    decimal Price,
    string PriceReasoning,
    int LifetimeInDays);