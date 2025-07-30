using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using PolicyProcessor.Contracts.Api;
using PolicyProcessor.Contracts.Models;
using PolicyProcessor.Infrastructure.Contracts.GetInsurancePolicy;

namespace PolicyProcessor.Host.Features.GetInsurancePolicy;

public class GetInsurancePolicy
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : InsurancePoliciesEndpointGroup<long, 
            GetInsurancePolicyResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get insurance policy info";

        public override int ApiVersion => 1;

        public override IEndpointHandler<long, GetInsurancePolicyResponse> Handler => _endpointHandler;

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
            : EndpointHandlerBase<long, GetInsurancePolicyRequestDto, GetInsurancePolicyResponse, GetInsurancePolicyResponseDto>(logger, mediator)
        {
            public override GetInsurancePolicyRequestDto MapRequest(long userId)
            {
                return new GetInsurancePolicyRequestDto(userId);
            }

            public override GetInsurancePolicyResponse MapResponse(GetInsurancePolicyResponseDto responseDto)
            {
                var insurancePlanDto = responseDto.InsurancePlan;
                var insurancePlan = new InsurancePlan(
                    insurancePlanDto.Id,
                    insurancePlanDto.Name,
                    insurancePlanDto.Price,
                    insurancePlanDto.PriceReasoning,
                    insurancePlanDto.LifetimeInDays,
                    insurancePlanDto.PolicyTemplateFileId);

                var insurancePolicy = new InsurancePolicy(
                    responseDto.Id,
                    responseDto.Status,
                    responseDto.UserId,
                    responseDto.VehicleId,
                    insurancePlan);
                return new GetInsurancePolicyResponse(insurancePolicy);
            }
        }
    }
}