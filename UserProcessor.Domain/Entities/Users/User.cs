using SharedKernel;
using SharedKernel.Enums;
using SharedKernel.Results;
using UserProcessor.Domain.Entities.Users.Requests;
using UserProcessor.Domain.Entities.Users.Responses;
using UserProcessor.Domain.Entities.Vehicles;

namespace UserProcessor.Domain.Entities.Users;

public sealed class User : Entity
{
    private User(
        Ulid id,
        UserStatus status,
        string phoneNumber) 
        : base (id)
    {
        Status = status;
        PhoneNumber = phoneNumber;
    }

    public UserStatus Status { get; private set; }

    public string PhoneNumber { get; }

    public UserPassport? Passport { get; set; }

    public ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();

    public static Result<CreateNewUserResponse> CreateNewUser(CreateNewUserRequest request)
    {
        // Validate received request.
        var validationResult = request.ValidateRequest();
        if (validationResult.IsFailure)
        {
            return validationResult.ToGenericFailureResult<CreateNewUserResponse>();
        }

        var newUser = new User(
            Ulid.NewUlid(), 
            UserStatus.Created,
            request.PhoneNumber);

        var response = new CreateNewUserResponse(newUser);
        return Result.Success(response);
    }
}