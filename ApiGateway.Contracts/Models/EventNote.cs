using SharedKernel.BaseTypes;

namespace ApiGateway.Contracts.Models;

public record EventNote(
    Ulid Id,
    Ulid UserId,
    EventBase OccuredEvent,
    DateTime CreatedOn);