using FluentValidation;
using SharedKernel.Requests;
using UserProcessor.Domain.Entities.Users;

namespace UserProcessor.Domain.Entities.Vehicles.Requests;

public class RegisterNewVehicleRequest : RequestBase<RegisterNewVehicleRequest>
{
    public required User User { get; init; }

    public required Guid FileId { get; init; }

    public required string OwnerName { get; init; }

    public required string VehicleName { get; init; }

    public required string VehicleIdentificationNumber { get; init; }

    public required string PlateNumber { get; init; }

    public required string RegistrationNumber { get; init; }


    protected override IValidator<RegisterNewVehicleRequest> Validator =>
        new RegisterNewVehicleRequestValidator();

    private class RegisterNewVehicleRequestValidator : AbstractValidator<RegisterNewVehicleRequest>
    {
        public RegisterNewVehicleRequestValidator()
        {
            RuleFor(request => request.User.Status)
                .Equal(UserStatus.Confirmed)
                .WithMessage("Unable to register vehicle for unconfirmed user");

            RuleFor(request => request.FileId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid file identifier");

            RuleFor(request => request.OwnerName)
                .Must((request, ownerName) => request.User.Passport!.FullName == ownerName)
                .WithMessage("Vehicle owner name doesn't match the name of the user it being registered for");

            // TODO: check other properties
        }
    }
}