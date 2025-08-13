using MassTransit.Mediator;
using SharedKernel.Enums;
using SharedKernel.Results;

namespace DocumentProcessor.Infrastructure.Contracts.GetDocument;

public record GetDocumentRequestDto(
    DocumentType DocumentType,
    Ulid FileId)
    : Request<Result<GetDocumentResponseDto>>;