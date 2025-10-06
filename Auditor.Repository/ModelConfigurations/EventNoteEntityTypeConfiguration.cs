using Auditor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Infrastructure.Converters;
using Repository.Infrastructure.EntityTypeConfigurators;
using SharedKernel.BaseTypes;

namespace Auditor.Repository.ModelConfigurations;

public class EventNoteEntityTypeConfiguration
    : ContainerEntityTypeConfigurationBase<EventNote>
{
    protected override bool IsDiscriminatorRequired => false;

    protected override void ConfigureInner(EntityTypeBuilder<EventNote> builder)
    {
        //builder
        //    .HasOne(eventNote => eventNote.OccuredEvent);

        //builder
        //    .OwnsOne(eventNote => eventNote.OccuredEvent);

        //builder
        //    .ComplexProperty(eventNote => eventNote.OccuredEvent)
        //    .IsRequired();

        builder
            .Property(eventNote => eventNote.OccuredEvent)
            .HasConversion<ObjectToJsonConverter<EventBase>>()
            .IsRequired();
    }
}