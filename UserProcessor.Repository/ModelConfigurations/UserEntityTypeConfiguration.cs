using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel.Enums;
using UserProcessor.Domain.Entities.Users;

namespace UserProcessor.Repository.ModelConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table name.
        builder.ToTable("Users");

        // Primary key.
        builder.HasKey(user => user.Id);

        builder
            .Property(user => user.Id)
            // According to Ulid specification.
            .HasMaxLength(26)
            .ValueGeneratedNever();

        // Relationships.
        builder
            .HasOne(user => user.Passport)
            .WithOne()
            .HasForeignKey<UserPassport>(userPassport => userPassport.UserId)
            .IsRequired();
        builder
            .Navigation(user => user.Passport)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder
            .HasMany(user => user.Vehicles)
            .WithOne()
            .HasForeignKey(vehicle => vehicle.UserId)
            .IsRequired();
        builder
            .Navigation(user => user.Vehicles)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        // Properties.
        builder
            .Property(user => user.Status)
            .HasConversion<EnumToStringConverter<UserStatus>>()
            // Should be more than enough for enum field.
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(user => user.PhoneNumber)
            // According to E.164 standard.
            .HasMaxLength(15)
            .IsRequired();
    }
}