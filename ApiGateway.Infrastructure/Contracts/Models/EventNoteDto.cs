using SharedKernel.BaseTypes;

namespace ApiGateway.Infrastructure.Contracts.Models;

public record EventNoteDto(
    Ulid Id,
    Ulid UserId,
    EventBase OccuredEvent,
    DateTime CreatedOn);