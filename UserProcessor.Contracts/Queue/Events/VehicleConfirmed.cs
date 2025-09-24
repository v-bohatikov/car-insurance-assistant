using SharedKernel.BaseTypes;
using UserProcessor.Contracts.Models;

namespace UserProcessor.Contracts.Queue.Events;

public record VehicleConfirmed(
    Ulid UserId,
    Vehicle Vehicle)
    : EventBase(UserId);