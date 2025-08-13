using SharedKernel.Enums;

// ReSharper disable once CheckNamespace
namespace DocumentProcessor.Contracts.Api;

public record GetDocumentRequest(
    DocumentType DocumentType,
    Ulid FileId);