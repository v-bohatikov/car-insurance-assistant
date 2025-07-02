using FluentValidation;
using FluentValidation.Results;
using SharedKernel.Results;
using System.Text;

namespace SharedKernel.Extensions;

public static class ValidationExtensions
{
    public static Result ToResult(this ValidationResult validationResult)
    {
        return validationResult.IsValid
            ? Result.Success()
            : Result.Failure(ToError(validationResult.Errors));
    }

    private static Error ToError(IList<ValidationFailure> validationFailures)
    {
        // Handle the case for single validation failure.
        if (validationFailures.Count == 1)
        {
            var validationFailure = validationFailures.First();
            return Error.Validation(
                "Validation.Failure",
                validationFailure.ErrorMessage);
        }

        // Handle the case with multiple validation failures.
        var validationDescription = new StringBuilder();
        for (var i = 0; i < validationFailures.Count; i++)
        {
            var validationFailure = validationFailures[i];

            if (i != 0)
            {
                validationDescription.AppendLine();
            }

            validationDescription.Append($"{validationFailure.ErrorCode} : {validationFailure.ErrorMessage}");
        }

        return Error.Validation(
            "Validation.Failures",
            validationDescription.ToString());
    }

    /// <summary>
    /// Validate phone number in E.164 format.
    /// </summary>
    public static IRuleBuilderOptions<T, string> MustBeValidPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Matches(@"^\+[1-9]\d{1,14}$")
            .WithMessage("Incorrect phone number format");
    }
}