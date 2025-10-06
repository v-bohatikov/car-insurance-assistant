using ApiGateway.Contracts.Models;

namespace ApiGateway.Contracts.Api.GetUserEvents;

public record GetUserEventsResponse(
    IReadOnlyCollection<EventNote> EventNotes);