using ApiGateway.Contracts.Api.UserContactProvided;
using ApiGateway.Infrastructure.Contracts.UserContactProvided;
using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Host.Features.UserContactProvidedTemp;

public class UserContactProvidedTemp
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : ApiEndpointBase<UserContactProvidedRequest, UserContractProvidedResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Produce user contact information";

        public override int ApiVersion => 1;

        public override IApiEndpointHandler<UserContactProvidedRequest, UserContractProvidedResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<UserContactProvidedRequest> requestHandler)
        {
            return builder.MapPost(
                "users/sendUserContact",
                ([FromBody] UserContactProvidedRequest userContactProvided, CancellationToken cancellationToken) =>
                    requestHandler(userContactProvided, cancellationToken));
        }

        private sealed class EndpointHandler(
            ILogger logger,
            IMediator mediator)
            : ApiEndpointHandlerBase<
                UserContactProvidedRequest, UserContactProvidedRequestDto,
                UserContractProvidedResponse, UserContactProvidedResponseDto>(logger, mediator)
        {
            public override UserContactProvidedRequestDto MapRequest(UserContactProvidedRequest userContactProvided)
            {
                return new UserContactProvidedRequestDto(
                    userContactProvided.UserTelegramId,
                    userContactProvided.PhoneNumber);
            }

            public override UserContractProvidedResponse MapResponse(UserContactProvidedResponseDto response)
            {
                return new UserContractProvidedResponse();
            }
        }
    }
}