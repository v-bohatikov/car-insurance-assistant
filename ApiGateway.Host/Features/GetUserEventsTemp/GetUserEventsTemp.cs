using ApiGateway.Contracts.Api.GetUserEvents;
using ApiGateway.Contracts.Models;
using ApiGateway.Infrastructure.Contracts.GetUserEvents;
using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Host.Features.GetUserEventsTemp;

public class GetUserEventsTemp
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : ApiEndpointBase<Ulid, GetUserEventsResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get user events";

        public override int ApiVersion => 1;

        public override IApiEndpointHandler<Ulid, GetUserEventsResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<Ulid> requestHandler)
        {
            return builder.MapGet(
                "users/{userId}/events",
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