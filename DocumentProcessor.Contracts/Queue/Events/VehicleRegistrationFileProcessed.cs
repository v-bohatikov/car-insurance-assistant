using DocumentProcessor.Contracts.Models;

namespace DocumentProcessor.Contracts.Queue.Events;

public record VehicleRegistrationFileProcessed(
    Ulid UserId,
    Ulid FileId,
    VehicleRegistrationData VehicleRegistrationData);