using SharedKernel.Enums;

namespace OrderProcessor.Contracts.Models;

public record Order(
    Ulid Id,
    OrderStatus Status,
    Ulid UserId,
    Ulid VehicleId,
    Ulid InsurancePlanId);