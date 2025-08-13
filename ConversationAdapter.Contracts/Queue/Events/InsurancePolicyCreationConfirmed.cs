namespace ConversationAdapter.Contracts.Queue.Events;

public record InsurancePolicyCreationConfirmed(
    Ulid UserId,
    Ulid VehicleId,
    Ulid InsurancePlanId);