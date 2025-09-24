using Auditor.Infrastructure.Contracts.EventReceived;
using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using UserContactProvidedEvent = ConversationAdapter.Contracts.Queue.Events.UserContactProvided;

namespace Auditor.Host.Features.UserContactProvided;

public class UserContactProvided
{
    public class Endpoint(
        ILogger<Endpoint> logger,
        IServiceBusMessageConverter messageConverter,
        IMediator mediator)
        : ServiceBusEndpointBase<UserContactProvidedEvent, ReceivedEventDto>(logger, messageConverter, mediator)
    {
        public override ReceivedEventDto MapQueueMessage(UserContactProvidedEvent queueMessage)
        {
            return new ReceivedEventDto(queueMessage);
        }
    }
}