using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using SharedKernel.Results;
using UserProcessor.Infrastructure.Contracts.UserContactProvided;

namespace UserProcessor.Application.Consumers;

public class UserContactProvidedConsumer(ILogger<UserContactProvidedConsumer> logger)
    : MediatorRequestHandler<UserContactProvidedDto, Result>
{
    protected override Task<Result> Handle(
        UserContactProvidedDto request,
        CancellationToken cancellationToken)
    {
        var error = new Error(
            ErrorType.Failure,
            "Error.NotSupported",
            "Method is not yet supported");
        return Task.FromResult(Result.Failure(error));
    }
}