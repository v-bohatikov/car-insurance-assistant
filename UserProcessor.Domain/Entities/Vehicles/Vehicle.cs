using SharedKernel;
using SharedKernel.Enums;
using SharedKernel.Results;
using UserProcessor.Domain.Entities.Vehicles.Requests;
using UserProcessor.Domain.Entities.Vehicles.Responses;

namespace UserProcessor.Domain.Entities.Vehicles;

public sealed class Vehicle : Entity
{
    private Vehicle(
        Ulid id,
        Ulid userId,
        VehicleStatus status,
        Ulid documentId,
        string ownerName,
        string vehicleName,
        string vehicleIdentificationNumber,
        string plateNumber,
        string registrationNumber)
        : base(id)
    {
        UserId = userId;
        Status = status;
        DocumentId = documentId;
        OwnerName = ownerName;
        VehicleName = vehicleName;
        VehicleIdentificationNumber = vehicleIdentificationNumber;
        PlateNumber = plateNumber;
        RegistrationNumber = registrationNumber;
    }

    public Ulid UserId { get; }

    public VehicleStatus Status { get; private set; }

    public Ulid DocumentId { get; }

    public string OwnerName { get; }

    public string VehicleName { get; }

    public string VehicleIdentificationNumber { get; }

    public string PlateNumber { get; }

    public string RegistrationNumber { get; }

    public static Result<RegisterNewVehicleResponse> RegisterNewVehicle(RegisterNewVehicleRequest registerNewVehicleRequest)
    {
        // Validate received request.
        var validationResult = registerNewVehicleRequest.ValidateRequest();
        if (validationResult.IsFailure)
        {
            return validationResult.ToGenericFailureResult<RegisterNewVehicleResponse>();
        }

        // TODO:
        throw new NotSupportedException();
    }
}