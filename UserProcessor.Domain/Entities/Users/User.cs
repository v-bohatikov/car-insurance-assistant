using SharedKernel;
using SharedKernel.Enums;
using SharedKernel.Results;
using UserProcessor.Domain.Entities.Users.Requests;
using UserProcessor.Domain.Entities.Users.Responses;

namespace UserProcessor.Domain.Entities.Users;

public sealed class User : Entity
{
    private User(
        Ulid id,
        UserStatus status,
        string phoneNumber,
        UserPassport? passport)
        : base (id)
    {
        Status = status;
        PhoneNumber = phoneNumber;
        Passport = passport;
    }

    public UserStatus Status { get; set; }

    public string PhoneNumber { get; set; }

    public UserPassport? Passport { get; set; }

    public static Result<CreateNewUserResponse> CreateNewUser(CreateNewUserRequest request)
    {
        // Validate received request.
        var validationResult = request.ValidateRequest();
        if (validationResult.IsFailure)
        {
            return validationResult.ToGenericFailureResult<CreateNewUserResponse>();
        }

        // TODO: temporary logic
        var newUser = new User(
            Ulid.NewUlid(), 
            UserStatus.Created,
            request.PhoneNumber,
            null);
        var response = new CreateNewUserResponse(newUser);

        return Result.Success(response);
    }
}