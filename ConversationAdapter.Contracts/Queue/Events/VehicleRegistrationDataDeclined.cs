namespace ConversationAdapter.Contracts.Queue.Events;

public record VehicleRegistrationDataDeclined(
    Ulid UserId,
    Ulid FileId);