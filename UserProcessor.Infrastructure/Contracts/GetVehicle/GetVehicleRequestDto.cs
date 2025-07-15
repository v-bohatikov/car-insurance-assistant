using MassTransit.Mediator;
using SharedKernel.Results;

namespace UserProcessor.Infrastructure.Contracts.GetVehicle;

public record GetVehicleRequestDto(
    long UserId,
    long VehicleId)
    : Request<Result<GetVehicleResponseDto>>;