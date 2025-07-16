using Infrastructure.Abstractions;
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
        : OrdersEndpointGroup<long, GetOrderResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get order info";

        public override int ApiVersion => 1;

        public override IEndpointHandler<long, GetOrderResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<long> requestHandler)
        {
            return builder.MapGet(
                "{id:long}",
                ([FromRoute] long id, CancellationToken cancellationToken) =>
                    requestHandler(id, cancellationToken));
        }

        private sealed class EndpointHandler(
            ILogger logger,
            IMediator mediator)
            : EndpointHandlerBase<long, GetOrderRequestDto, GetOrderResponse, GetOrderResponseDto>(logger, mediator)
        {
            public override GetOrderRequestDto MapRequest(long userId)
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