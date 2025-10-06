using SharedKernel.Enums;

namespace OrderProcessor.Infrastructure.Contracts.GetOrder;

public record GetOrderResponseDto(
    Ulid Id,
    OrderStatus Status,
    Ulid UserId,
    Ulid VehicleId,
    Ulid InsurancePlanId,
    string? FailureReasoning);