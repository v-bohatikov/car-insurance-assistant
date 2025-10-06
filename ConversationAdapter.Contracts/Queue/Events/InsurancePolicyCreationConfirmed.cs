using SharedKernel.BaseTypes;

namespace ConversationAdapter.Contracts.Queue.Events;

public record InsurancePolicyCreationConfirmed(
    Ulid UserId,
    Ulid VehicleId,
    Ulid InsurancePlanId)
    : EventBase(UserId);