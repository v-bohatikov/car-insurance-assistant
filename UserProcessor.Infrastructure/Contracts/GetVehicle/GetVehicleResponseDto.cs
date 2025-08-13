using SharedKernel.Enums;

namespace UserProcessor.Infrastructure.Contracts.GetVehicle;

public record GetVehicleResponseDto(
    Ulid Id,
    VehicleStatus Status,
    string OwnerName,
    string VehicleName,
    string VehicleIdentificationNumber,
    string PlateNumber,
    string RegistrationNumber);