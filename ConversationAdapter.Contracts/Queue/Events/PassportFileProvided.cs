namespace ConversationAdapter.Contracts.Queue.Events;

public record PassportFileProvided(
    long UserId,
    string UploadedFilePath);