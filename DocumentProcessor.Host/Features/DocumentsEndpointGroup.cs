using Host.Infrastructure.Abstractions;

namespace DocumentProcessor.Host.Features;

public abstract class DocumentsEndpointGroup<TRequest, TResponse>(
    ILogger<DocumentsEndpointGroup<TRequest, TResponse>> logger) :
    EndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "documents";
}