namespace Host.Infrastructure.Abstractions;

public interface IServiceBusMessageSender
{
    ValueTask SendAsync<TMessage>(
        string queueReference,
        TMessage message,
        CancellationToken cancellationToken)
        where TMessage : class;
}