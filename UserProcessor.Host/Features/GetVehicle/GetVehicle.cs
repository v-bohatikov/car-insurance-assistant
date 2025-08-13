using Host.Infrastructure.Abstractions;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using UserProcessor.Contracts.Api;
using UserProcessor.Contracts.Models;
using UserProcessor.Infrastructure.Contracts.GetVehicle;

namespace UserProcessor.Host.Features.GetVehicle
{
    public class GetVehicle
    {
        public sealed class Endpoint(
            ILogger<Endpoint> logger,
            IMediator mediator)
            : UsersEndpointGroup<GetVehicleRequest, GetVehicleResponse>(logger)
        {
            private readonly EndpointHandler _endpointHandler = new(logger, mediator);

            public override string? Name => "Get user's vehicle info";

            public override int ApiVersion => 1;

            public override IEndpointHandler<GetVehicleRequest, GetVehicleResponse> Handler => _endpointHandler;

            protected override RouteHandlerBuilder MapEndpoint(
                IEndpointRouteBuilder builder,
                HandleEndpointRequestDelegate<GetVehicleRequest> requestHandler)
            {
                return builder.MapGet(
                    "{userId}/vehicles/{vehicleId}",
                    ([FromRoute] Ulid userId, [FromRoute] Ulid vehicleId, CancellationToken cancellationToken) =>
                        requestHandler(new GetVehicleRequest(userId, vehicleId), cancellationToken));
            }

            private sealed class EndpointHandler(
                ILogger logger,
                IMediator mediator)
                : EndpointHandlerBase<GetVehicleRequest, GetVehicleRequestDto, GetVehicleResponse, GetVehicleResponseDto>(logger, mediator)
            {
                public override GetVehicleRequestDto MapRequest(GetVehicleRequest request)
                {
                    return new GetVehicleRequestDto(
                        request.UserId,
                        request.VehicleId);
                }

                public override GetVehicleResponse MapResponse(GetVehicleResponseDto responseDto)
                {
                    var vehicle = new Vehicle(
                        responseDto.Id,
                        responseDto.Status,
                        responseDto.OwnerName,
                        responseDto.VehicleName,
                        responseDto.VehicleIdentificationNumber,
                        responseDto.PlateNumber,
                        responseDto.RegistrationNumber);
                    return new GetVehicleResponse(vehicle);
                }
            }
        }
    }
}
