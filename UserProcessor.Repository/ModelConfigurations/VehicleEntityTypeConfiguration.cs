using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel.Enums;
using UserProcessor.Domain.Entities.Vehicles;

namespace UserProcessor.Repository.ModelConfigurations;

public class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        // Table name.
        builder.ToTable("Vehicles");

        // Primary key.
        builder.HasKey(vehicle => vehicle.Id);

        builder
            .Property(vehicle => vehicle.Id)
            // According to Ulid specification.
            .HasMaxLength(26)
            .ValueGeneratedNever();

        // Relationships.
        // Because the only relation is with 'User' entity we don't need to duplicate it here.

        // Properties.
        builder
            .Property(vehicle => vehicle.UserId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(vehicle => vehicle.DocumentId)
            // According to Ulid specification.
            .HasMaxLength(26)
            .IsRequired();

        builder
            .Property(vehicle => vehicle.Status)
            .HasConversion<EnumToStringConverter<VehicleStatus>>()
            // Should be more than enough for enum field.
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(vehicle => vehicle.OwnerName)
            // There is no specific standard for it.
            .HasMaxLength(60)
            .IsRequired();

        builder
            .Property(vehicle => vehicle.VehicleName)
            // There is no specific standard for it.
            .HasMaxLength(40)
            .IsRequired();

        builder
            .Property(vehicle => vehicle.VehicleIdentificationNumber)
            // According to the standard regarding VIN.
            .HasMaxLength(17)
            .IsRequired();

        builder
            .Property(vehicle => vehicle.PlateNumber)
            // There is no specific standard for it.
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(vehicle => vehicle.RegistrationNumber)
            // There is no specific standard for it.
            .HasMaxLength(10)
            .IsRequired();
    }
}