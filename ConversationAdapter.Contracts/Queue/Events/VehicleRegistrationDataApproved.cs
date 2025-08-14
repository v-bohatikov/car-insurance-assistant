using ConversationAdapter.Contracts.Models;

namespace ConversationAdapter.Contracts.Queue.Events;

public record VehicleRegistrationDataApproved(
    Ulid UserId,
    Ulid DocumentId,
    VehicleRegistrationData VehicleRegistrationData);