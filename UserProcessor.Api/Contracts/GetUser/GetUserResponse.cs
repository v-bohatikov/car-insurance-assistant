using UserProcessor.Api.Contracts.Models;

namespace UserProcessor.Api.Contracts.GetUser;

public record GetUserResponse(
    long Id,
    UserStatus Status,
    string PhoneNumber,
    UserPassport Passport);