using DocumentProcessor.Infrastructure.Abstractions;
using DocumentProcessor.Infrastructure.Contracts.GetDocument;
using MassTransit.Mediator;
using SharedKernel.Results;

namespace DocumentProcessor.Application.Consumers;

public class GetDocumentRequestConsumer(IDocumentQueryService documentQueryService)
    : MediatorRequestHandler<GetDocumentRequestDto, Result<GetDocumentResponseDto>>
{
    protected override async Task<Result<GetDocumentResponseDto>> Handle(
        GetDocumentRequestDto request,
        CancellationToken cancellationToken)
    {
        return await documentQueryService.GetDocument(request);
    }
}