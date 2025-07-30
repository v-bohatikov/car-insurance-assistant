using ConversationAdapter.Infrastructure.Contracts.ProcessCustomerAction;
using SharedKernel.Results;

namespace ConversationAdapter.Infrastructure.Abstractions;

public interface ICustomerActonProcessorService
{
    ValueTask<Result<CustomerActionResponseDto>> ProcessCustomerAction(CustomerActionRequestDto request);
}