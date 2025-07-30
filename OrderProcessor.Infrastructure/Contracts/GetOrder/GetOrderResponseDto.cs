using SharedKernel.Enums;

namespace OrderProcessor.Infrastructure.Contracts.GetOrder;

public record GetOrderResponseDto(
    long Id,
    OrderStatus Status,
    long UserId,
    long VehicleId,
    long InsurancePlanId,
    string? FailureReasoning);