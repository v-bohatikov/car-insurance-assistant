using SharedKernel.BaseTypes;

namespace ConversationAdapter.Contracts.Queue.Events;

public record VehicleRegistrationFileProvided(
    Ulid UserId,
    string UploadedFilePath)
    : EventBase(UserId);