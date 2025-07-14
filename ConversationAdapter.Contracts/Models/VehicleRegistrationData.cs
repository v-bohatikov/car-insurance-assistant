namespace ConversationAdapter.Contracts.Models;

public record VehicleRegistrationData(
    string OwnerName,
    string VehicleName,
    string VehicleIdentificationNumber,
    string PlateNumber,
    string RegistrationNumber);