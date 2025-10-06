using SharedKernel.BaseTypes;

namespace Auditor.Contracts.Models;

public record EventNote(
    Ulid Id,
    Ulid UserId,
    EventBase OccuredEvent,
    DateTime CreatedOn);