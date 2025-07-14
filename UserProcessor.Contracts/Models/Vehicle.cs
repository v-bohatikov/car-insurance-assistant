using SharedKernel.Enums;

namespace UserProcessor.Contracts.Models;

public record Vehicle(
    long Id,
    VehicleStatus Status,
    string OwnerName,
    string VehicleName,
    string VehicleIdentificationNumber,
    string PlateNumber,
    string RegistrationNumber);