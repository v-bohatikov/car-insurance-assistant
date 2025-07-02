namespace SharedKernel.Results;

public record Error(ErrorType ErrorType, string Code, string? Description = null)
{
    public static Error None =>
        new (ErrorType.None, string.Empty);

    public static Error Validation(string code, string? description) =>
        new(ErrorType.Validation, code, description);

    public static Error NotFound(string code, string? description) =>
        new(ErrorType.NotFound, code, description);

    public static Error Failure(string code, string? description) =>
        new(ErrorType.Failure, code, description);

}