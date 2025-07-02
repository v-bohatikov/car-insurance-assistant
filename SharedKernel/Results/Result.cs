using System.Data;

namespace SharedKernel.Results;

public class Result
{
    protected Result(bool isSuccessful, Error error)
    {
        if (isSuccessful && error != Error.None ||
            !isSuccessful && error == Error.None)
        {
            throw new ArgumentException(
                "Invalid result status of the operation");
        }

        IsSuccessful = isSuccessful;
        Error = error;
    }

    public bool IsSuccessful { get; init; }

    public bool IsFailure => !IsSuccessful;

    public Error? Error { get; init; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) => Result<TValue>.Success(value);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) => Result<TValue>.Failure(error);

    public Result<TValue> ToGenericResult<TValue>()
    {
        // This type of conversion is something that we need exclusively for cases when
        // need to propagate the failure result further, where generic result type
        // is being required.
        if (IsSuccessful)
        {
            throw new ArgumentException(
                "Unable to convert the successful result to the generic format");
        }

        return Failure<TValue>(Error!);
    }
}

public sealed class Result<TValue> : Result
{
    private readonly TValue? _value;

    private Result(TValue? value, bool isSuccessful, Error error)
        : base(isSuccessful, error)
    {
        if (isSuccessful && value is null ||
            !isSuccessful && value is not null)
        {
            throw new ArgumentException(
                "Invalid result status of the operation");
        }

        _value = value;
    }

    public TValue Value
    {
        get
        {
            if (IsFailure)
            {
                throw new ConstraintException(
                    "Unable to retrieve a result from failed operation");
            }

            return _value!;
        }
    }

    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    public new static Result<TValue> Failure(Error error) => new(default, false, error);
}