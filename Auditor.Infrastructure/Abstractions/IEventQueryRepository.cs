using Auditor.Domain.Entities;

namespace Auditor.Infrastructure.Abstractions;

public interface IEventQueryRepository
{
    ValueTask<bool> ContainsEventByIdAsync(
        Ulid userId,
        Ulid eventId,
        CancellationToken cancellationToken);

    ValueTask<EventNote?> GetEventByEventIdAsync(
        Ulid userId,
        Ulid eventId,
        CancellationToken cancellationToken);

    ValueTask<IReadOnlyCollection<EventNote>> GetEventsByUserIdAsync(
        Ulid userId,
        CancellationToken cancellationToken);
}