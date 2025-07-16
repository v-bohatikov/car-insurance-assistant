using SharedKernel.Enums;

// ReSharper disable once CheckNamespace
namespace ConversationAdapter.Contracts.Api;

public record CustomerActionRequest(
    CustomerActionType ActionType,
    long? UserId,
    SystemCommandType? CommandType,
    string? PhoneNumber,
    string? UploadedFilePath,
    string? Message);