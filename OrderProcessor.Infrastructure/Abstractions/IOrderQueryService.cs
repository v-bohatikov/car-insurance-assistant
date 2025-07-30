using OrderProcessor.Infrastructure.Contracts.GetOrder;
using SharedKernel.Results;

namespace OrderProcessor.Infrastructure.Abstractions;

public interface IOrderQueryService
{
    ValueTask<Result<GetOrderResponseDto>> GetOrder(GetOrderRequestDto request);
}