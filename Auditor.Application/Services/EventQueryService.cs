using Auditor.Infrastructure.Abstractions;
using Auditor.Infrastructure.Contracts.GetUserEvents;
using Auditor.Infrastructure.Contracts.Models;
using Microsoft.Extensions.Logging;
using SharedKernel.Extensions;
using SharedKernel.Results;

namespace Auditor.Application.Services;

public class EventQueryService(
    ILogger<EventStorageService> logger,
    IEventQueryRepository queryRepository)
    : IEventQueryService
{
    public async ValueTask<Result<GetUserEventsResponseDto>> GetEventsByUserIdAsync(
        GetUserEventsRequestDto getUserEventsRequest,
        CancellationToken cancellationToken)
    {
        var userId = getUserEventsRequest.UserId;

        logger.LogInformation(
            "Starting the collection of events for user with {UserId} id",
            userId);

        var eventsByUserId = await queryRepository
            .GetEventsByUserIdAsync(userId, cancellationToken);
        if (eventsByUserId.IsNullOrEmpty())
        {
            var error = new Error(
                ErrorType.NotFound,
                "Error.NotFound",
                $"There are no events found for user with {userId} id");
            return Result.Failure<GetUserEventsResponseDto>(error);
        }

        logger.LogInformation(
            "Collection of events for user with {UserId} id has finished, {EventsCount} event(s) has been collected",
            userId, eventsByUserId.Count);

        var eventNoteDtos = eventsByUserId
            .Select(eventNote => new EventNoteDto(
                eventNote.Id,
                eventNote.UserId,
                eventNote.OccuredEvent,
                eventNote.CreatedOn))
            .ToList();
        var getUserEventsResponse = new GetUserEventsResponseDto(eventNoteDtos);

        return Result.Success(getUserEventsResponse);
    }
}