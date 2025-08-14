using MassTransit.Mediator;
using SharedKernel.Results;

namespace DocumentProcessor.Infrastructure.Contracts.GetDocument;

public record GetDocumentRequestDto(Ulid DocumentId)
    : Request<Result<GetDocumentResponseDto>>;