using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using UserProcessor.Infrastructure.Contracts.UserContactProvided;
using UserContactProvidedEvent = ConversationAdapter.Contracts.Queue.Events.UserContactProvided;

namespace UserProcessor.Host.Features.UserContactProvided;

public class UserContactProvided
{
    public class Endpoint(
        ILogger<Endpoint> logger,
        IServiceBusMessageConverter messageConverter,
        IMediator mediator)
        : ServiceBusEndpointBase<UserContactProvidedEvent, UserContactProvidedDto>(logger, messageConverter, mediator)
    {
        public override UserContactProvidedDto MapQueueMessage(UserContactProvidedEvent queueMessage)
        {
            return new UserContactProvidedDto(
                queueMessage.UserTelegramId,
                queueMessage.PhoneNumber);
        }
    }
}