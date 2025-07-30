using ConversationAdapter.Infrastructure.Abstractions;
using ConversationAdapter.Infrastructure.Contracts.ProcessCustomerAction;
using SharedKernel.Results;

namespace ConversationAdapter.Application.Services;

public class CustomerActonProcessorService : ICustomerActonProcessorService
{
    public async ValueTask<Result<CustomerActionResponseDto>> ProcessCustomerAction(CustomerActionRequestDto request)
    {
        var error = Error.Failure(
            "Error.NotSupported",
            "This method is not supported");
        return Result.Failure<CustomerActionResponseDto>(error);
    }
}