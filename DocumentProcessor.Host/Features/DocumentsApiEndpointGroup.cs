using Host.Infrastructure.Abstractions;

namespace DocumentProcessor.Host.Features;

public abstract class DocumentsApiEndpointGroup<TRequest, TResponse>(
    ILogger<DocumentsApiEndpointGroup<TRequest, TResponse>> logger) :
    ApiEndpointGroupBase<TRequest, TResponse>(logger)
{
    public sealed override string GroupName => "documents";
}