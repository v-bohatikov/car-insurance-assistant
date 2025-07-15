using SharedKernel.Enums;

namespace ConversationAdapter.Contracts.Api.ProcessCustomerAction;

public record CustomerActionRequest(
    CustomerActionType ActionType,
    long? UserId,
    SystemCommandType? CommandType,
    string? PhoneNumber,
    string? UploadedFilePath,
    string? Message);