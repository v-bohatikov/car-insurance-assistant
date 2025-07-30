using MassTransit.Mediator;
using SharedKernel.Results;

namespace OrderProcessor.Infrastructure.Contracts.GetOrder;

public record GetOrderRequestDto(long OrderId)
    : Request<Result<GetOrderResponseDto>>;