using Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using UserProcessor.Contracts.Api.GetUser;
using UserProcessor.Contracts.Models;
using UserProcessor.Infrastructure.Contracts.GetUser;

namespace UserProcessor.Host.Features.GetUser;

public class GetUser
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : UsersEndpointGroup<long, GetUserResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "Get user info";

        public override int ApiVersion => 1;

        public override IEndpointHandler<long, GetUserResponse> Handler => _endpointHandler;

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
            : EndpointHandlerBase<long, GetUserRequestDto, GetUserResponse, GetUserResponseDto>(logger, mediator)
        {
            public override GetUserRequestDto MapRequest(long userId)
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