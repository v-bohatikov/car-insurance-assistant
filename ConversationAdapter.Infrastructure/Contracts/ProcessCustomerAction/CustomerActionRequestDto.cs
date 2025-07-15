using MassTransit.Mediator;
using SharedKernel.Enums;
using SharedKernel.Results;

namespace ConversationAdapter.Infrastructure.Contracts.ProcessCustomerAction;

public record CustomerActionRequestDto(
    CustomerActionType ActionType,
    long? UserId,
    SystemCommandType? CommandType,
    string? PhoneNumber,
    string? UploadedFilePath,
    string? Message)
    : Request<Result<CustomerActionResponseDto>>;