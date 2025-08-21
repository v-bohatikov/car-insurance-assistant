using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel;

namespace Repository.Infrastructure.EntityTypeConfigurators;

public abstract class EntityTypeConfigurationBase<TEntity>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureInner(builder);

        ConfigureAuditableFields(builder);
    }

    public abstract void ConfigureInner(EntityTypeBuilder<TEntity> builder);

    private void ConfigureAuditableFields(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property(entity => entity.CreatedOn)
            .HasConversion<DateTimeToStringConverter>()
            // According to converter hints.
            .HasMaxLength(48)
            .IsRequired();

        builder
            .Property(entity => entity.ChangedOn)
            .HasConversion<DateTimeToStringConverter>()
            // According to converter hints.
            .HasMaxLength(48)
            .IsRequired(false);
    }
}