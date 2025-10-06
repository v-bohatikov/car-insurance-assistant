using MassTransit.Mediator;
using SharedKernel.Results;

namespace OrderProcessor.Infrastructure.Contracts.GetOrder;

public record GetOrderRequestDto(Ulid OrderId)
    : Request<Result<GetOrderResponseDto>>;