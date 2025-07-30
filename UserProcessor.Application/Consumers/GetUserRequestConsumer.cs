using MassTransit.Mediator;
using SharedKernel.Results;
using UserProcessor.Infrastructure.Abstractions;
using UserProcessor.Infrastructure.Contracts.GetUser;

namespace UserProcessor.Application.Consumers;

public class GetUserRequestConsumer(IUserQueryService userQueryService) 
    : MediatorRequestHandler<GetUserRequestDto, Result<GetUserResponseDto>>
{
    protected override async Task<Result<GetUserResponseDto>> Handle(
        GetUserRequestDto request,
        CancellationToken cancellationToken)
    {
        return await userQueryService.GetUser(request);
    }
}