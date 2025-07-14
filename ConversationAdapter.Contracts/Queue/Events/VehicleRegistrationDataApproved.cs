using ConversationAdapter.Contracts.Models;

namespace ConversationAdapter.Contracts.Queue.Events;

public record VehicleRegistrationDataApproved(
    long UserId,
    Guid FileId,
    VehicleRegistrationData VehicleRegistrationData);