// ReSharper disable once CheckNamespace
namespace UserProcessor.Contracts.Api;

public record GetVehicleRequest(
    long UserId,
    long VehicleId);