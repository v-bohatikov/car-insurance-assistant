using UserProcessor.Contracts.Models;

namespace UserProcessor.Contracts.Queue.Events;

public record UserCreated(User User);