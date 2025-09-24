using Application.Infrastructure.Abstractions;
using Host.Infrastructure.Abstractions;
using Host.Infrastructure.Settings;

namespace Host.Infrastructure.Services;

public class QueueMessageSenderBase(
    IServiceBusMessageSender serviceBusMessageSender,
    string queueReference)
    : IQueueMessageSender
{
    public string TargetQueueName => queueReference;

    public async ValueTask SendAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken)
        where TMessage : class
    {
        await serviceBusMessageSender.SendAsync(
            queueReference,
            message,
            cancellationToken);
    }
}

public class UserQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.UserQueueResourceName), IUserQueueMessageSender
{ }

public class AuditorQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.AuditorQueueResourceName), IAuditorQueueMessageSender
{ }

public class DocumentQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.DocumentQueueResourceName), IDocumentQueueMessageSender
{ }

public class PolicyQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.PolicyQueueResourceName), IPolicyQueueMessageSender
{ }

public class OrderQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.OrderQueueResourceName), IOrderQueueMessageSender
{ }

public class BillingQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.BillingQueueResourceName), IBillingQueueMessageSender
{ }

public class ConversationQueueMessageSender(IServiceBusMessageSender serviceBusMessageSender)
    : QueueMessageSenderBase(serviceBusMessageSender, ApplicationReferences.ConversationQueueResourceName), IConversationQueueMessageSender
{ }