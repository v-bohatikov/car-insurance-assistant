using FluentValidation;
using SharedKernel.Extensions;
using SharedKernel.Requests;

namespace UserProcessor.Domain.Entities.Users.Requests;

public class CreateNewUserRequest : RequestBase<CreateNewUserRequest>
{
    public required long UserTelegramId { get; init; }

    public required string PhoneNumber { get; init; }

    protected override IValidator<CreateNewUserRequest> Validator =>
        new CreateNewUserRequestValidator();

    private class CreateNewUserRequestValidator : AbstractValidator<CreateNewUserRequest>
    {
        public CreateNewUserRequestValidator()
        {
            RuleFor(request => request.PhoneNumber)
                .MustBeValidPhoneNumber();

            // TODO: is there a rule for validation of telegram id?
        }
    }
}