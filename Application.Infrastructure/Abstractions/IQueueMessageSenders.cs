namespace Application.Infrastructure.Abstractions;

public interface IQueueMessageSender
{
    string TargetQueueName { get; }

    ValueTask SendAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken)
        where TMessage : class;
}

public interface IUserQueueMessageSender : IQueueMessageSender
{ }

public interface IAuditorQueueMessageSender : IQueueMessageSender
{ }

public interface IDocumentQueueMessageSender : IQueueMessageSender
{ }

public interface IPolicyQueueMessageSender : IQueueMessageSender
{ }

public interface IOrderQueueMessageSender : IQueueMessageSender
{ }

public interface IBillingQueueMessageSender : IQueueMessageSender
{ }

public interface IConversationQueueMessageSender : IQueueMessageSender
{ }