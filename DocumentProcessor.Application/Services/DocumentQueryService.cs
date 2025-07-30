using DocumentProcessor.Infrastructure.Abstractions;
using DocumentProcessor.Infrastructure.Contracts.GetDocument;
using SharedKernel.Results;

namespace DocumentProcessor.Application.Services;

public class DocumentQueryService : IDocumentQueryService
{
    public async ValueTask<Result<GetDocumentResponseDto>> GetDocument(GetDocumentRequestDto request)
    {
        var error = Error.Failure(
            "Error.NotSupported",
            "This method is not supported");
        return Result.Failure<GetDocumentResponseDto>(error);
    }
}