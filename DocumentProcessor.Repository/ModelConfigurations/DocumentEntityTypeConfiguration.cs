using DocumentProcessor.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel.Enums;

namespace DocumentProcessor.Repository.ModelConfigurations;

public class DocumentEntityTypeConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        // Table name.
        builder.ToTable("Documents");

        // Primary key.
        builder.HasKey(document => document.Id);

        builder
            .Property(document => document.Id)
            // According to Ulid specification.
            .HasMaxLength(26)
            .ValueGeneratedNever();

        // Relationships.
        // No relationships here.

        // Properties.
        builder
            .Property(document => document.FileId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(document => document.DocumentType)
            .HasConversion<EnumToStringConverter<DocumentType>>()
            // Should be more than enough for enum field.
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(document => document.DocumentDownloadUrl)
            // Should be more than enough for url.
            .HasMaxLength(500)
            .IsRequired(false);

        // Indexes.
        // No indexes required.

        // Seeding.
        builder.HasData(GetSeedingData());
    }

    private static ICollection<Document> GetSeedingData()
    {
        const string basicPolicyTemplateDocumentId = "01K33RW7YFCVHMWJM3BT9VSHF8";
        const string basicPolicyTemplateFileId = "01K33SHJFHVVW9AC1YEC8FJ129";

        const string premiumPolicyTemplateDocumentId = "01K33RWMYKQTSCGBXMWZTQCCKK";
        const string premiumPolicyTemplateFileId = "01K33SKD4PKGDZCG5S73NDD4PP";

        var insurancePolicyTemplateDocuments = new[]
        {
            Document.CreateInsurancePolicyTemplateDocument(
                Ulid.Parse(basicPolicyTemplateDocumentId),
                Ulid.Parse(basicPolicyTemplateFileId)),

            Document.CreateInsurancePolicyTemplateDocument(
                Ulid.Parse(premiumPolicyTemplateDocumentId),
                Ulid.Parse(premiumPolicyTemplateFileId)),
        };

        return insurancePolicyTemplateDocuments;
    }
}