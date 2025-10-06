using SharedKernel.BaseTypes;

namespace Tests.Models;

public record SpecificEvent(
    Ulid UserId,
    string Description)
    : EventBase(UserId);


public record EvenMoreSpecificEvent(
    Ulid UserId,
    string Description,
    string Note)
    : SpecificEvent(UserId, Description);