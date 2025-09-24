using SharedKernel.BaseTypes;

namespace Auditor.Infrastructure.Contracts.Models;

public record EventNoteDto(
    Ulid Id,
    Ulid UserId,
    EventBase OccuredEvent,
    DateTime CreatedOn);