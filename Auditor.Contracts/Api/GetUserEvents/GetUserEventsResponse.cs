using Auditor.Contracts.Models;

// ReSharper disable once CheckNamespace
namespace Auditor.Contracts.Api;

public record GetUserEventsResponse(
    IReadOnlyCollection<EventNote> EventNotes);