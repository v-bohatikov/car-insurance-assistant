using SharedKernel.Enums;

namespace UserProcessor.Contracts.Models;

public record User(
    long Id,
    UserStatus Status,
    string PhoneNumber,
    UserPassport? Passport);