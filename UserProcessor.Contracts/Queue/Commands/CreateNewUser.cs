namespace UserProcessor.Contracts.Queue.Commands;

public record CreateNewUser(
    string UserId,
    string PhoneNumber);