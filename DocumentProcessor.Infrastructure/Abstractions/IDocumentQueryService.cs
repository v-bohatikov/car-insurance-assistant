using DocumentProcessor.Infrastructure.Contracts.GetDocument;
using SharedKernel.Results;

namespace DocumentProcessor.Infrastructure.Abstractions;

public interface IDocumentQueryService
{
    ValueTask<Result<GetDocumentResponseDto>> GetDocument(GetDocumentRequestDto request);
}