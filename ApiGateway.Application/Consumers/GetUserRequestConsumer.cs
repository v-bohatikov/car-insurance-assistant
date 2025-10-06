using ApiGateway.Infrastructure.Abstractions;
using ApiGateway.Infrastructure.Contracts.GetUser;
using MassTransit.Mediator;
using SharedKernel.Results;

namespace ApiGateway.Application.Consumers;

public class GetUserRequestConsumer(IQueryService userQueryService) 
    : MediatorRequestHandler<GetUserRequestDto, Result<GetUserResponseDto>>
{
    protected override async Task<Result<GetUserResponseDto>> Handle(
        GetUserRequestDto request,
        CancellationToken cancellationToken)
    {
        return await userQueryService.GetUserAsync(
            request, cancellationToken);
    }
}