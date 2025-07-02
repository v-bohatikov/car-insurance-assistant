namespace SharedKernel.Results;

public sealed class ErrorType : IEquatable<ErrorType>
{
    private readonly string _errorType;

    private ErrorType(string errorType)
    {
        _errorType = errorType;
    }

    /// <summary>
    /// Represents the reserved value for indication that error didn't happen.
    /// </summary>
    internal static ErrorType None { get; } = new("None");

    /// <summary>
    /// Represent an unexpected exception that have occured during code execution.
    /// </summary>
    public static ErrorType Failure { get; } = new("Failure");

    /// <summary>
    /// Represents an error that have occured during validation of the request.
    /// </summary>
    public static ErrorType Validation { get; } = new("Validation");

    /// <summary>
    /// Represents an error that have occured during the attempt to access application resource.
    /// </summary>
    public static ErrorType NotFound { get; } = new("NotFound");

    /// <summary>
    /// Defines the equality operator for the error type.
    /// </summary>
    /// <param name="type1">Error type to equate with</param>
    /// <param name="type2">Error type to equate to</param>
    /// <returns></returns>
    public static bool operator ==(ErrorType type1, ErrorType type2)
    {
        return Equals(type1, type2);
    }

    /// <summary>
    /// Defines the opposition operator for the error type.
    /// </summary>
    /// <param name="type1">Error type to equate with</param>
    /// <param name="type2">Error type to equate to</param>
    /// <returns></returns>
    public static bool operator !=(ErrorType type1, ErrorType type2)
    {
        return !Equals(type1, type2);
    }

    /// <summary>
    /// Implicit conversion to string representation.
    /// </summary>
    /// <param name="errorType">
    /// Error type object to be converted.
    /// </param>
    public static implicit operator string(ErrorType errorType) => errorType._errorType;

    public override string ToString()
    {
        // Will use the operator for conversion to string representation.
        return this;
    }

    public bool Equals(ErrorType? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return _errorType == other._errorType;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) ||
               obj is ErrorType other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _errorType.GetHashCode();
    }
}