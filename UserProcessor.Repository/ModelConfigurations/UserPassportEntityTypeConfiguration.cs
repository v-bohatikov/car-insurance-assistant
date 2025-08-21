using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Infrastructure.EntityTypeConfigurators;
using SharedKernel.Enums;
using UserProcessor.Domain.Entities.Users;

namespace UserProcessor.Repository.ModelConfigurations;

public class UserPassportEntityTypeConfiguration
    : EntityTypeConfigurationBase<UserPassport>
{
    public override void ConfigureInner(EntityTypeBuilder<UserPassport> builder)
    {
        // Table name.
        builder.ToTable("UserPassports");

        // Primary key.
        builder.HasKey(userPassport => userPassport.Id);

        builder
            .Property(userPassport => userPassport.Id)
            // According to Ulid specification.
            .HasMaxLength(26)
            .ValueGeneratedNever();

        // Relationships.
        // Because the only relation is with 'User' entity we don't need to duplicate it here.

        // Properties.
        builder
            .Property(userPassport => userPassport.UserId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(userPassport => userPassport.DocumentId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(userPassport => userPassport.Surname)
            // There is no specific limitations regarding surname in MRZ section,
            // only that total character limit(surname + given names) is 39.
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(userPassport => userPassport.GivenNames)
            // There is no specific limitations regarding surname in MRZ section,
            // // only that total character limit(surname + given names) is 39.
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(userPassport => userPassport.PassportNumber)
            // According to US passport standards.
            .HasMaxLength(9)
            .IsRequired();

        builder
            .Property(userPassport => userPassport.Sex)
            .HasConversion<EnumToStringConverter<Sex>>()
            // Should be more than enough for enum field.
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(userPassport => userPassport.DateOfBirth)
            .HasConversion<DateOnlyToStringConverter>()
            // According to converter hints.
            .HasMaxLength(10);

        builder
            .Ignore(userPassport => userPassport.FullName);

        // Indexes.
        builder
            .HasIndex(userPassport => userPassport.PassportNumber)
            .IsUnique();

        // Seeding.
        // No seeding required.
    }
}