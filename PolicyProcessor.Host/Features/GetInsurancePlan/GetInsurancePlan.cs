using Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using PolicyProcessor.Contracts.Api;
using PolicyProcessor.Contracts.Models;
using PolicyProcessor.Infrastructure.Contracts.GetInsurancePlan;

namespace PolicyProcessor.Host.Features.GetInsurancePlan;

public class GetInsurancePlan
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : InsurancePlansEndpointGroup<long, GetInsurancePlanResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get insurance plan info";

        public override int ApiVersion => 1;

        public override IEndpointHandler<long, GetInsurancePlanResponse> Handler => _endpointHandler;

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
            : EndpointHandlerBase<long, GetInsurancePlanRequestDto, GetInsurancePlanResponse, GetInsurancePlanResponseDto>(logger, mediator)
        {
            public override GetInsurancePlanRequestDto MapRequest(long userId)
            {
                return new GetInsurancePlanRequestDto(userId);
            }

            public override GetInsurancePlanResponse MapResponse(GetInsurancePlanResponseDto responseDto)
            {
                var insurancePlan = new InsurancePlan(
                    responseDto.Id,
                    responseDto.Name,
                    responseDto.Price,
                    responseDto.PriceReasoning,
                    responseDto.LifetimeInDays,
                    responseDto.PolicyTemplateFileId);
                return new GetInsurancePlanResponse(insurancePlan);
            }
        }
    }
}