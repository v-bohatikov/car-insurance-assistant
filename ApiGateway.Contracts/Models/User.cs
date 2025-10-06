using SharedKernel.Enums;

namespace ApiGateway.Contracts.Models;

public record User(
    Ulid Id,
    UserStatus Status,
    string PhoneNumber,
    UserPassport? Passport);