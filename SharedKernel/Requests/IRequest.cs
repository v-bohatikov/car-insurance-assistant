using FluentValidation;
using SharedKernel.Extensions;
using SharedKernel.Results;

namespace SharedKernel.Requests;

public interface IRequest<TRequest>
    where TRequest : IRequest<TRequest>
{
    Result ValidateRequest();
}

public abstract class RequestBase<TRequest> : IRequest<TRequest>
    where TRequest : RequestBase<TRequest>
{
    public Result ValidateRequest()
    {
        return Validator.Validate(Request)!.ToResult();
    }

    protected abstract IValidator<TRequest> Validator { get; }

    private TRequest Request => (this as TRequest)!;
}