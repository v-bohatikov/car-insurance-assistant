using DocumentProcessor.Contracts.Models;

namespace DocumentProcessor.Contracts.Queue.Events;

public record VehicleRegistrationFileProcessed(
    long UserId,
    Guid FileId,
    VehicleRegistrationData VehicleRegistrationData);