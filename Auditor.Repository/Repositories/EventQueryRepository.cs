using Auditor.Domain.Entities;
using Auditor.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Auditor.Repository.Repositories;

public class EventQueryRepository(AuditorDbContext dbContext)
    : IEventQueryRepository
{
    public async ValueTask<bool> ContainsEventByIdAsync(
        Ulid userId,
        Ulid eventId,
        CancellationToken cancellationToken)
    {
        var eventCount = await GetEventNotesQueryable(userId)
            .CountAsync(
                eventNote => eventNote.Id == eventId,
                cancellationToken);

        return eventCount != 0;
    }

    public async ValueTask<EventNote?> GetEventByEventIdAsync(
        Ulid userId,
        Ulid eventId,
        CancellationToken cancellationToken)
    {
        return await GetEventNotesQueryable(userId)
            .FirstOrDefaultAsync(
                eventNote => eventNote.Id == eventId,
                cancellationToken);
    }

    public async ValueTask<IReadOnlyCollection<EventNote>> GetEventsByUserIdAsync(
        Ulid userId,
        CancellationToken cancellationToken)
    {
        return await GetEventNotesQueryable(userId)
            .ToListAsync(cancellationToken);
    }

    private IQueryable<EventNote> GetEventNotesQueryable(Ulid userId)
    {
        // Building a queryable which filters by a partition key,
        // 'UserId' in this case.
        return dbContext.Events
            .Where(eventNote => eventNote.UserId == userId)
            .AsNoTracking()
            .AsQueryable();
    }
}