using ApiGateway.Infrastructure.Abstractions;
using ApiGateway.Infrastructure.Contracts.GetUserEvents;
using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Application.Consumers;

public class GetUserEventsRequestConsumer(IQueryService userQueryService) 
    : MediatorRequestHandler<GetUserEventsRequestDto, Result<GetUserEventsResponseDto>>
{
    protected override async Task<Result<GetUserEventsResponseDto>> Handle(
        GetUserEventsRequestDto request,
        CancellationToken cancellationToken)
    {
        return await userQueryService.GetUserEventsAsync(
            request, cancellationToken);
    }
}