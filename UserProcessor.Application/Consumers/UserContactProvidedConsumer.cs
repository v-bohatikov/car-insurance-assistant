using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using UserProcessor.Infrastructure.Contracts.UserContactProvided;

namespace UserProcessor.Application.Consumers;

public class UserContactProvidedConsumer(ILogger<UserContactProvidedConsumer> logger)
    : MediatorRequestHandler<UserContactProvidedDto>
{
    protected override Task Handle(
        UserContactProvidedDto request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "UserContractProvided event has been received");
        return Task.CompletedTask;
    }
}