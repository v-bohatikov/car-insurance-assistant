using Auditor.Domain.Entities;

namespace Auditor.Infrastructure.Abstractions;

public interface IEventStorageRepository
{
    ValueTask AddEventNoteAsync(
        EventNote eventNote,
        CancellationToken cancellationToken);

    ValueTask SaveChangesAsync(CancellationToken cancellationToken);
}