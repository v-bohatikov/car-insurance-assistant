using OrderProcessor.Infrastructure.Abstractions;
using OrderProcessor.Infrastructure.Contracts.GetOrder;
using SharedKernel.Results;

namespace OrderProcessor.Application.Services;

public class OrderQueryService : IOrderQueryService
{
    public async ValueTask<Result<GetOrderResponseDto>> GetOrder(GetOrderRequestDto request)
    {
        var error = Error.Failure(
            "Error.NotSupported",
            "This method is not supported");
        return Result.Failure<GetOrderResponseDto>(error);
    }
}