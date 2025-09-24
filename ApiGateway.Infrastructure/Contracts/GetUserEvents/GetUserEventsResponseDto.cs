using ApiGateway.Infrastructure.Contracts.Models;

namespace ApiGateway.Infrastructure.Contracts.GetUserEvents;

public record GetUserEventsResponseDto(
    IReadOnlyCollection<EventNoteDto> EventNotes);