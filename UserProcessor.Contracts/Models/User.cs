using SharedKernel.Enums;

namespace UserProcessor.Contracts.Models;

public record User(
    Ulid Id,
    UserStatus Status,
    string PhoneNumber,
    UserPassport? Passport);