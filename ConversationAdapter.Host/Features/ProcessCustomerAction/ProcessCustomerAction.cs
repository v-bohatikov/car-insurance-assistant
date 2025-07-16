using ConversationAdapter.Contracts.Api;
using ConversationAdapter.Infrastructure.Contracts.ProcessCustomerAction;
using Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ConversationAdapter.Host.Features.ProcessCustomerAction;

public class ProcessCustomerAction
{
    public sealed class Endpoint(
    ILogger<Endpoint> logger,
    IMediator mediator)
    : CustomerActionEndpointGroup<CustomerActionRequest, CustomerActionResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Process customer action";

        public override int ApiVersion => 1;

        public override IEndpointHandler<CustomerActionRequest, CustomerActionResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<CustomerActionRequest> requestHandler)
        {
            return builder.MapGet(
                "process",
                ([FromBody] CustomerActionRequest request, CancellationToken cancellationToken) =>
                    requestHandler(request, cancellationToken));
        }

        private sealed class EndpointHandler(
            ILogger logger,
            IMediator mediator)
            : EndpointHandlerBase<
                CustomerActionRequest, CustomerActionRequestDto,
                CustomerActionResponse, CustomerActionResponseDto>(logger, mediator)
        {
            public override CustomerActionRequestDto MapRequest(CustomerActionRequest request)
            {
                return new CustomerActionRequestDto(
                    request.ActionType,
                    request.UserId,
                    request.CommandType,
                    request.PhoneNumber,
                    request.UploadedFilePath,
                    request.Message);
            }

            public override CustomerActionResponse MapResponse(CustomerActionResponseDto responseDto)
            {
                return new CustomerActionResponse(
                    responseDto.ImmediateResponse);
            }
        }
    }

}