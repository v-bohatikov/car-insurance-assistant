using SharedKernel.Enums;

namespace ApiGateway.Contracts.Models;

public record User(
    long Id,
    UserStatus Status,
    string PhoneNumber,
    UserPassport? Passport);