using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using OrderProcessor.Contracts.Api;
using OrderProcessor.Contracts.Models;
using OrderProcessor.Infrastructure.Contracts.GetOrder;

namespace OrderProcessor.Host.Features.GetOrder;

public class GetOrder
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : OrdersApiEndpointGroup<Ulid, GetOrderResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get order info";

        public override int ApiVersion => 1;

        public override IApiEndpointHandler<Ulid, GetOrderResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<Ulid> requestHandler)
        {
            return builder.MapGet(
                "{id}",
                ([FromRoute] Ulid id, CancellationToken cancellationToken) =>
                    requestHandler(id, cancellationToken));
        }

        private sealed class EndpointHandler(
            ILogger logger,
            IMediator mediator)
            : ApiEndpointHandlerBase<Ulid, GetOrderRequestDto, GetOrderResponse, GetOrderResponseDto>(logger, mediator)
        {
            public override GetOrderRequestDto MapRequest(Ulid userId)
            {
                return new GetOrderRequestDto(userId);
            }

            public override GetOrderResponse MapResponse(GetOrderResponseDto responseDto)
            {
                var order = new Order(
                    responseDto.Id,
                    responseDto.Status,
                    responseDto.UserId,
                    responseDto.VehicleId,
                    responseDto.InsurancePlanId);
                return new GetOrderResponse(order);
            }
        }
    }

}