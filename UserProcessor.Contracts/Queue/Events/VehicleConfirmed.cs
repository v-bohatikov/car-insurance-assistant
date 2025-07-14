using UserProcessor.Contracts.Models;

namespace UserProcessor.Contracts.Queue.Events;

public record VehicleConfirmed(
    long UserId,
    Vehicle Vehicle);