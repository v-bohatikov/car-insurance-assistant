using SharedKernel.BaseTypes;

namespace DocumentProcessor.Contracts.Queue.Events;

public record VehicleRegistrationFileProcessingFailed(Ulid UserId)
    : EventBase(UserId);