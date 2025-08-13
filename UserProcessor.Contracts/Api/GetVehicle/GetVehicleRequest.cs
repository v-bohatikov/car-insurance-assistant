// ReSharper disable once CheckNamespace
namespace UserProcessor.Contracts.Api;

public record GetVehicleRequest(
    Ulid UserId,
    Ulid VehicleId);