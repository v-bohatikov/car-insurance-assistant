using DocumentProcessor.Contracts.Models;
using SharedKernel.BaseTypes;

namespace DocumentProcessor.Contracts.Queue.Events;

public record VehicleRegistrationFileProcessed(
    Ulid UserId,
    Ulid DocumentId,
    VehicleRegistrationData VehicleRegistrationData)
    : EventBase(UserId);