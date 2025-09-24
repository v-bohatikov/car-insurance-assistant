using Application.Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;
using SharedKernel.BaseTypes;

namespace Host.Infrastructure.Services;

public class AuditingEventSenderBase(
    ILogger logger,
    IQueueMessageSender targetQueueMessageSender,
    IAuditorQueueMessageSender auditorQueueMessageSender)
    : IEventSender
{
    public async ValueTask SendAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken)
        where TEvent : EventBase
    {
        // TODO: There is only an intent of what we need in the result. The question is how to achieve
        // that goal and keep our system reliable?

        var eventType = @event.GetType();
        logger.LogInformation("Sending an {EventType} event to {TargetQueueName} queue.",
            eventType, targetQueueMessageSender.TargetQueueName);
        await targetQueueMessageSender.SendAsync(@event, cancellationToken);

        // Because we are not able to use sub/pub model for Azure ServiceBus,
        // we need to send a duplicate of event to the Auditor service.
        // TODO: is there any better way to do that?
        logger.LogInformation("Sending an {EventType} event to {TargetQueueName} queue.",
            eventType, auditorQueueMessageSender.TargetQueueName);
        await auditorQueueMessageSender.SendAsync(@event, cancellationToken);
    }
}

public class UserEventSender(
    ILogger<UserEventSender> logger,
    IUserQueueMessageSender userQueueMessageSender,
    IAuditorQueueMessageSender auditorQueueMessageSender)
    : AuditingEventSenderBase(logger, userQueueMessageSender, auditorQueueMessageSender), IUserEventSender;

public class DocumentEventSender(
    ILogger<DocumentEventSender> logger,
    IDocumentQueueMessageSender documentQueueMessageSender,
    IAuditorQueueMessageSender auditorQueueMessageSender)
    : AuditingEventSenderBase(logger, documentQueueMessageSender, auditorQueueMessageSender), IDocumentEventSender;

public class PolicyEventSender(
    ILogger<PolicyEventSender> logger,
    IPolicyQueueMessageSender policyQueueMessageSender,
    IAuditorQueueMessageSender auditorQueueMessageSender)
    : AuditingEventSenderBase(logger, policyQueueMessageSender, auditorQueueMessageSender), IPolicyEventSender;

public class OrderEventSender(
    ILogger<OrderEventSender> logger,
    IOrderQueueMessageSender orderQueueMessageSender,
    IAuditorQueueMessageSender auditorQueueMessageSender)
    : AuditingEventSenderBase(logger, orderQueueMessageSender, auditorQueueMessageSender), IOrderEventSender;

public class BillingEventSender(
    ILogger<BillingEventSender> logger,
    IBillingQueueMessageSender billingQueueMessageSender,
    IAuditorQueueMessageSender auditorQueueMessageSender)
    : AuditingEventSenderBase(logger, billingQueueMessageSender, auditorQueueMessageSender), IBillingEventSender;

public class ConversationEventSender(
    ILogger<ConversationEventSender> logger,
    IConversationQueueMessageSender conversationQueueMessageSender,
    IAuditorQueueMessageSender auditorQueueMessageSender)
    : AuditingEventSenderBase(logger, conversationQueueMessageSender, auditorQueueMessageSender), IConversationEventSender;
