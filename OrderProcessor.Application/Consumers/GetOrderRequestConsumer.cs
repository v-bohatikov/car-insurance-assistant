using MassTransit.Mediator;
using OrderProcessor.Infrastructure.Abstractions;
using OrderProcessor.Infrastructure.Contracts.GetOrder;
using SharedKernel.Results;

namespace OrderProcessor.Application.Consumers;

public class GetOrderRequestConsumer(IOrderQueryService orderQueryService)
    : MediatorRequestHandler<GetOrderRequestDto, Result<GetOrderResponseDto>>
{
    protected override async Task<Result<GetOrderResponseDto>> Handle(
        GetOrderRequestDto request,
        CancellationToken cancellationToken)
    {
        return await orderQueryService.GetOrder(request);
    }
}