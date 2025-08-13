using MassTransit.Mediator;
using SharedKernel.Results;

namespace UserProcessor.Infrastructure.Contracts.GetVehicle;

public record GetVehicleRequestDto(
    Ulid UserId,
    Ulid VehicleId)
    : Request<Result<GetVehicleResponseDto>>;