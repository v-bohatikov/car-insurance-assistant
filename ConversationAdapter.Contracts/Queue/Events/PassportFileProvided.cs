using SharedKernel.BaseTypes;

namespace ConversationAdapter.Contracts.Queue.Events;

public record PassportFileProvided(
    Ulid UserId,
    string UploadedFilePath)
    : EventBase(UserId);