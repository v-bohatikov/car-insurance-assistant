using SharedKernel.BaseTypes;

namespace DocumentProcessor.Contracts.Queue.Events;

public record PassportFileProcessingFailed(Ulid UserId)
    : EventBase(UserId);