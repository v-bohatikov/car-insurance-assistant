using FluentValidation;
using SharedKernel.Extensions;
using SharedKernel.Requests;
using SharedKernel.Results;

namespace UserProcessor.Domain.Entities.Users.Requests;

public class CreateNewUserRequest : RequestBase<CreateNewUserRequest>
{
    public required string PhoneNumber { get; init; }

    protected override IValidator<CreateNewUserRequest> Validator =>
        new CreateNewUserRequestValidator();

    private class CreateNewUserRequestValidator : AbstractValidator<CreateNewUserRequest>
    {
        public CreateNewUserRequestValidator()
        {
            RuleFor(request => request.PhoneNumber)
                .MustBeValidPhoneNumber();
        }
    }
}