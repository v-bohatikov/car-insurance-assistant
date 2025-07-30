using MassTransit.Mediator;
using SharedKernel.Results;
using UserProcessor.Infrastructure.Abstractions;
using UserProcessor.Infrastructure.Contracts.GetVehicle;

namespace UserProcessor.Application.Consumers;

public class GetVehicleRequestConsumer(IUserQueryService userQueryService) 
    : MediatorRequestHandler<GetVehicleRequestDto, Result<GetVehicleResponseDto>>
{
    protected override async Task<Result<GetVehicleResponseDto>> Handle(
        GetVehicleRequestDto request,
        CancellationToken cancellationToken)
    {
        return await userQueryService.GetVehicle(request);
    }
}