using Auditor.Infrastructure.Contracts.Models;

namespace Auditor.Infrastructure.Contracts.GetUserEvents;

public record GetUserEventsResponseDto(
    IReadOnlyCollection<EventNoteDto> EventNotes);