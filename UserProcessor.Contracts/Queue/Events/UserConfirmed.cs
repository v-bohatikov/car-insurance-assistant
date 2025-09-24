using SharedKernel.BaseTypes;
using UserProcessor.Contracts.Models;

namespace UserProcessor.Contracts.Queue.Events;

public record UserConfirmed(User User)
    : EventBase(User.Id);