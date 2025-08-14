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
            .Property(order => order.FileId)
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
    }
}