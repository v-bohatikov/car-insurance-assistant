using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel;

namespace Repository.Infrastructure.EntityTypeConfigurators;

public abstract class ContainerEntityTypeConfigurationBase<TContainerEntity>
    : IEntityTypeConfiguration<TContainerEntity>
    where TContainerEntity : ContainerEntity
{
    protected abstract bool IsDiscriminatorRequired { get; }

    public void Configure(EntityTypeBuilder<TContainerEntity> builder)
    {
        ConfigureBaseFields(builder);

        ConfigureInner(builder);

        ConfigureAuditableFields(builder);
    }

    private void ConfigureBaseFields(EntityTypeBuilder<TContainerEntity> builder)
    {
        builder.HasKey(
            containerEntity => containerEntity.Id);
        builder
            .Property(containerEntity => containerEntity.Id)
            .ValueGeneratedNever()
            .IsRequired();

        // Partition key should be the same as the one which was used for container creation.
        builder.HasPartitionKey(
            containerEntity => containerEntity.UserId);
        builder
            .Property(containerEntity => containerEntity.UserId)
            .IsRequired();

        if (IsDiscriminatorRequired)
        {
            builder.HasDiscriminator<string>(
                containerEntity => containerEntity.Discriminator!);
            builder
                .Property(containerEntity => containerEntity.Discriminator)
                .IsRequired();
        }
        else
        {
            builder.HasNoDiscriminator();
        }
    }

    protected abstract void ConfigureInner(EntityTypeBuilder<TContainerEntity> builder);

    private void ConfigureAuditableFields(EntityTypeBuilder<TContainerEntity> builder)
    {
        builder
            .Property(containerEntity => containerEntity.CreatedOn)
            .HasConversion<DateTimeToStringConverter>()
            .IsRequired();

        builder
            .Property(containerEntity => containerEntity.ChangedOn)
            .HasConversion<DateTimeToStringConverter>()
            .IsRequired(false);
    }
}