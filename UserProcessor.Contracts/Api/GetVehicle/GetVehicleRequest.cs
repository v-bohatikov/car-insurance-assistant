namespace UserProcessor.Contracts.Api.GetVehicle;

public record GetVehicleRequest(
    long UserId,
    long VehicleId);