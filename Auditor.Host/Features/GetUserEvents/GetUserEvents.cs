using Auditor.Contracts.Api;
using Auditor.Contracts.Models;
using Auditor.Infrastructure.Contracts.GetUserEvents;
using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Auditor.Host.Features.GetUserEvents;

public class GetUserEvents
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : EventsApiEndpointGroup<Ulid, GetUserEventsResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get events related to specific user";

        public override int ApiVersion => 1;

        public override IApiEndpointHandler<Ulid, GetUserEventsResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<Ulid> requestHandler)
        {
            return builder.MapGet(
                "{userId}",
                ([FromRoute] Ulid userId, CancellationToken cancellationToken) =>
                    requestHandler(userId, cancellationToken));
        }

        private sealed class EndpointHandler(
            ILogger logger,
            IMediator mediator)
            : ApiEndpointHandlerBase<Ulid, GetUserEventsRequestDto, GetUserEventsResponse, GetUserEventsResponseDto>(logger, mediator)
        {
            public override GetUserEventsRequestDto MapRequest(Ulid userId)
            {
                return new GetUserEventsRequestDto(userId);
            }

            public override GetUserEventsResponse MapResponse(GetUserEventsResponseDto responseDto)
            {
                var eventNotes = responseDto.EventNotes
                    .Select(eventNoteDto => new EventNote(
                        eventNoteDto.Id,
                        eventNoteDto.UserId,
                        eventNoteDto.OccuredEvent,
                        eventNoteDto.CreatedOn))
                    .ToList();
                return new GetUserEventsResponse(eventNotes);
            }
        }
    }
}