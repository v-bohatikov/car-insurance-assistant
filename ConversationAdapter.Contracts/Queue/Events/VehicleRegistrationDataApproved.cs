using ConversationAdapter.Contracts.Models;
using SharedKernel.BaseTypes;

namespace ConversationAdapter.Contracts.Queue.Events;

public record VehicleRegistrationDataApproved(
    Ulid UserId,
    Ulid DocumentId,
    VehicleRegistrationData VehicleRegistrationData)
    : EventBase(UserId);