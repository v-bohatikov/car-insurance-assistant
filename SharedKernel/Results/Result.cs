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

    public static Result Success => new Result(true, Error.None);

    public static Result Failure(Error error) => new Result(false, error);
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

    public new static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    public new static Result<TValue> Failure(Error error) => new Result<TValue>(default, false, error);
}