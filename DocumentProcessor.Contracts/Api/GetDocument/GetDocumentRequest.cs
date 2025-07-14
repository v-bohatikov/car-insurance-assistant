using SharedKernel.Enums;

namespace DocumentProcessor.Contracts.Api.GetDocument;

public record GetDocumentRequest(
    DocumentType DocumentType,
    Guid FileId);