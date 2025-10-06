using ApiGateway.Contracts.Api.GetUser;
using ApiGateway.Contracts.Models;
using ApiGateway.Infrastructure.Contracts.GetUser;
using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Host.Features.GetUserTemp;

public class UserContactProvidedTemp
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : ApiEndpointBase<Ulid, GetUserResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get user info";

        public override int ApiVersion => 1;

        public override IApiEndpointHandler<Ulid, GetUserResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<Ulid> requestHandler)
        {
            return builder.MapGet(
                "users/{id}",
                ([FromRoute] Ulid id, CancellationToken cancellationToken) =>
                    requestHandler(id, cancellationToken));
        }

        private sealed class EndpointHandler(
            ILogger logger,
            IMediator mediator)
            : ApiEndpointHandlerBase<Ulid, GetUserRequestDto, GetUserResponse, GetUserResponseDto>(logger, mediator)
        {
            public override GetUserRequestDto MapRequest(Ulid userId)
            {
                return new GetUserRequestDto(userId);
            }

            public override GetUserResponse MapResponse(GetUserResponseDto responseDto)
            {
                UserPassport? userPassport = null;
                var responseDtoPassport = responseDto.Passport;
                if (responseDtoPassport is not null)
                {
                    userPassport = new UserPassport(
                        responseDtoPassport.Surname,
                        responseDtoPassport.GivenNames,
                        responseDtoPassport.PassportNumber,
                        responseDtoPassport.Sex,
                        responseDtoPassport.DateOfBirth);
                }

                var user = new User(
                    responseDto.Id,
                    responseDto.Status,
                    responseDto.PhoneNumber,
                    userPassport);
                return new GetUserResponse(user);
            }
        }
    }
}