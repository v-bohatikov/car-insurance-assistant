using Auditor.Domain.Entities;
using Auditor.Infrastructure.Abstractions;
using Auditor.Infrastructure.Contracts.EventReceived;
using Microsoft.Extensions.Logging;
using SharedKernel.Results;

namespace Auditor.Application.Services;

public class EventStorageService(
    ILogger<EventStorageService> logger,
    IEventQueryRepository queryRepository,
    IEventStorageRepository storageRepository)
    : IEventStorageService
{
    public async ValueTask<Result> StoreReceivedEventAsync(
        ReceivedEventDto receivedEvent,
        CancellationToken cancellationToken)
    {
        var occuredEvent = receivedEvent.ReceivedEvent;
        var occuredEventTypeName = occuredEvent.GetType().FullName;

        logger.LogInformation(
            "Storage of received event of {EventType} type has been started",
            occuredEventTypeName);

        // Check whether the event with the same id has been already created.
        var eventNote = EventNote.CreateEvent(occuredEvent);
        var isAlreadyExists = await queryRepository.ContainsEventByIdAsync(
            eventNote.UserId,
            eventNote.Id,
            cancellationToken);
        if (isAlreadyExists)
        {
            var error = Error.Validation(
                "Error.AlreadyExists",
                $"Event with the same {eventNote.Id} id is already registered. " +
                $"Event details: {eventNote}");
            return Result.Failure(error);
        }

        // Store the event information.
        await storageRepository.AddEventNoteAsync(eventNote, cancellationToken);
        await storageRepository.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Storage of received event of {EventType} type has been finished",
            occuredEventTypeName);

        return Result.Success();
    }
}