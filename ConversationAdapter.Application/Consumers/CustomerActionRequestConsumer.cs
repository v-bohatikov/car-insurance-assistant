using ConversationAdapter.Infrastructure.Abstractions;
using ConversationAdapter.Infrastructure.Contracts.ProcessCustomerAction;
using MassTransit.Mediator;
using SharedKernel.Results;

namespace ConversationAdapter.Application.Consumers;

public class CustomerActionRequestConsumer(ICustomerActonProcessorService customerActonProcessorService)
    : MediatorRequestHandler<CustomerActionRequestDto, Result<CustomerActionResponseDto>>
{
    protected override async Task<Result<CustomerActionResponseDto>> Handle(
        CustomerActionRequestDto request,
        CancellationToken cancellationToken)
    {
        return await customerActonProcessorService.ProcessCustomerAction(request);
    }
}