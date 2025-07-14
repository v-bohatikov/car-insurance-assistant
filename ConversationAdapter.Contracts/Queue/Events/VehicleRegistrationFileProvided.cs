namespace ConversationAdapter.Contracts.Queue.Events;

public record VehicleRegistrationFileProvided(
    long UserId,
    string UploadedFilePath);