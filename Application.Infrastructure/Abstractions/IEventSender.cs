using SharedKernel.BaseTypes;

namespace Application.Infrastructure.Abstractions;

public interface IEventSender
{
    ValueTask SendAsync<TMessage>(
        TMessage @event,
        CancellationToken cancellationToken)
        where TMessage : EventBase;
}

public interface IUserEventSender : IEventSender
{ }

public interface IDocumentEventSender : IEventSender
{ }

public interface IPolicyEventSender : IEventSender
{ }

public interface IOrderEventSender : IEventSender
{ }

public interface IBillingEventSender : IEventSender
{ }

public interface IConversationEventSender : IEventSender
{ }