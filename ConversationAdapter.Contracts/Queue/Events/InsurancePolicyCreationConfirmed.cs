namespace ConversationAdapter.Contracts.Queue.Events;

public record InsurancePolicyCreationConfirmed(
    long UserId,
    long VehicleId,
    long InsurancePlanId);