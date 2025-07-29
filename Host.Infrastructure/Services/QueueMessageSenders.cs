using Application.Infrastructure.Abstractions;
using Host.Infrastructure.Abstractions;
using Host.Infrastructure.Settings;

namespace Host.Infrastructure.Services;

public class QueueMessageSenderBase(
    IServiceBusMessageSender serviceBusMessageSender,
    string queueReference)
    : IQueueMessageSender
{
    public async ValueTask SendAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken)
        where TMessage : class
    {
        await serviceBusMessageSender.SendAsync(queueReference, message, cancellationToken);
    }
}

public class UserQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.UserQueueResourceName), IUserQueueMessageSender
{ }

public class AuditorQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.UserQueueResourceName), IAuditorQueueMessageSender
{ }

public class DocumentQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.UserQueueResourceName), IDocumentQueueMessageSender
{ }

public class PolicyQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.UserQueueResourceName), IPolicyQueueMessageSender
{ }

public class OrderQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.UserQueueResourceName), IOrderQueueMessageSender
{ }

public class BillingQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.UserQueueResourceName), IBillingQueueMessageSender
{ }

public class ConversationQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.UserQueueResourceName), IConversationQueueMessageSender
{ }