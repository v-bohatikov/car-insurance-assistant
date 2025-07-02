namespace SharedKernel.Results;

public record Error(ErrorType ErrorType, string Code, string? Description = null)
{
    public static Error None => new (ErrorType.None, string.Empty);
}