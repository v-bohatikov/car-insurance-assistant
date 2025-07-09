using Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Extensions;
using UserProcessor.Api.Contracts.GetUser;
using UserProcessor.Api.Contracts.Models;
using UserProcessor.Infrastructure.Contracts.GetUser;
using UserProcessor.Infrastructure.Contracts.Models;

namespace UserProcessor.Host.Features.GetUser;

public class GetUser
{
    public sealed class Endpoint(
        ILogger<Endpoint> logger,
        IMediator mediator)
        : UserEndpointGroup<long, GetUserResponse>(logger)
    {
        private readonly EndpointHandler _endpointHandler = new(logger, mediator);

        public override string? Name => "GetUser";

        public override int ApiVersion => 1;

        public override IEndpointHandler<long, GetUserResponse> Handler => _endpointHandler;

        protected override RouteHandlerBuilder MapEndpoint(
            IEndpointRouteBuilder builder,
            HandleEndpointRequestDelegate<long> requestHandler)
        {
            return builder.MapGet(
                "getUser/{id:long}",
                ([FromRoute] long id, CancellationToken cancellationToken) => requestHandler(id, cancellationToken));
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
                var userStatus = responseDto.Status
                    .MapSemantically<UserStatusDto, UserStatus>().Value;

                var responseDtoPassport = responseDto.Passport;
                var userPassport = new UserPassport(
                    responseDtoPassport.Surname,
                    responseDtoPassport.GivenNames,
                    responseDtoPassport.PassportNumber,
                    responseDtoPassport.Sex,
                    responseDtoPassport.DateOfBirth);

                return new GetUserResponse(
                    responseDto.Id,
                    userStatus,
                    responseDto.PhoneNumber,
                    userPassport);
            }
        }
    }
}