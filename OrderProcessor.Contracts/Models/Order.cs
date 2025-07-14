using SharedKernel.Enums;

namespace OrderProcessor.Contracts.Models;

public record Order(
    long Id,
    OrderStatus Status,
    long UserId,
    long VehicleId,
    long InsurancePlanId);