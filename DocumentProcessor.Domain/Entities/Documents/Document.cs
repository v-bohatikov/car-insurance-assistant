using SharedKernel;
using SharedKernel.Enums;

namespace DocumentProcessor.Domain.Entities.Documents;

public class Document : Entity
{
    private Document(
        Ulid id,
        DocumentType documentType,
        Ulid fileId)
        : base(id)
    {
        DocumentType = documentType;
        FileId = fileId;
    }

    public DocumentType DocumentType { get; }

    public Ulid FileId { get; }

    public string? DocumentDownloadUrl { get; private set; }

    public bool IsAvailableForDownload => DocumentType == DocumentType.InsurancePolicy;
}