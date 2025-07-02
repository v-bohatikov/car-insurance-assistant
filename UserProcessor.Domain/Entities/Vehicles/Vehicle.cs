using SharedKernel;
using SharedKernel.Results;
using UserProcessor.Domain.Entities.Vehicles.Requests;
using UserProcessor.Domain.Entities.Vehicles.Responses;

namespace UserProcessor.Domain.Entities.Vehicles;

public sealed class Vehicle : Entity
{
    public Vehicle(
        long id,
        long userId,
        VehicleStatus status,
        Guid fileId,
        string ownerName,
        string vehicleName,
        string vehicleIdentificationNumber,
        string plateNumber,
        string registrationNumber)
        : base(id)
    {
        UserId = userId;
        Status = status;
        FileId = fileId;
        OwnerName = ownerName;
        VehicleName = vehicleName;
        VehicleIdentificationNumber = vehicleIdentificationNumber;
        PlateNumber = plateNumber;
        RegistrationNumber = registrationNumber;
    }

    public long UserId { get; set; }

    public VehicleStatus Status { get; set; }

    public Guid FileId { get; set; }

    public string OwnerName { get; set; }

    public string VehicleName { get; set; }

    public string VehicleIdentificationNumber { get; set; }

    public string PlateNumber { get; set; }

    public string RegistrationNumber { get; set; }

    public static Result<RegisterNewVehicleResponse> RegisterNewVehicle(RegisterNewVehicleRequest registerNewVehicleRequest)
    {
        // Validate received request.
        var validationResult = registerNewVehicleRequest.ValidateRequest();
        if (validationResult.IsFailure)
        {
            return validationResult.ToGenericResult<RegisterNewVehicleResponse>();
        }

        // TODO:
        throw new NotSupportedException();
    }
}