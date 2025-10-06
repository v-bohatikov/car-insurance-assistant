using Auditor.Domain.Entities;
using Auditor.Infrastructure.Abstractions;

namespace Auditor.Repository.Repositories;

public class EventStorageRepository(AuditorDbContext dbContext)
    : IEventStorageRepository
{
    public async ValueTask AddEventNoteAsync(
        EventNote eventNote,
        CancellationToken cancellationToken)
    {
        await dbContext.Events.AddAsync(eventNote, cancellationToken);
    }

    public async ValueTask SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}