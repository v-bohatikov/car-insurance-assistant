using ApiGateway.Contracts.Api.GetInsurancePlan;
using ApiGateway.Contracts.Models;
using ApiGateway.Infrastructure.Contracts.GetInsurancePlan;
using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Host.Features.GetInsurancePlanTemp;

public class GetInsurancePlanTemp
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : EndpointBase<Ulid, GetInsurancePlanResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get insurance plan info";

        public override int ApiVersion => 1;

        public override IEndpointHandler<Ulid, GetInsurancePlanResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<Ulid> requestHandler)
        {
            return builder.MapGet(
                "insurancePlans/{id}",
                ([FromRoute] Ulid id, CancellationToken cancellationToken) =>
                    requestHandler(id, cancellationToken));
        }

        private sealed class EndpointHandler(
            ILogger logger,
            IMediator mediator)
            : EndpointHandlerBase<Ulid, GetInsurancePlanRequestDto, GetInsurancePlanResponse, GetInsurancePlanResponseDto>(logger, mediator)
        {
            public override GetInsurancePlanRequestDto MapRequest(Ulid insurancePlanId)
            {
                return new GetInsurancePlanRequestDto(insurancePlanId);
            }

            public override GetInsurancePlanResponse MapResponse(GetInsurancePlanResponseDto responseDto)
            {
                var insurancePlan = new InsurancePlan(
                    responseDto.Id,
                    responseDto.Name,
                    responseDto.Price,
                    responseDto.PriceReasoning,
                    responseDto.LifetimeInDays);
                return new GetInsurancePlanResponse(insurancePlan);
            }
        }
    }
}