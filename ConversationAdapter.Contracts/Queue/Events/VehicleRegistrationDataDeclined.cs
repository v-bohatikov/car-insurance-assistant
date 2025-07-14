namespace ConversationAdapter.Contracts.Queue.Events;

public record VehicleRegistrationDataDeclined(
    long UserId,
    Guid FileId);