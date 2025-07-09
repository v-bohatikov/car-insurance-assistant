using UserProcessor.Infrastructure.Contracts.Models;

namespace UserProcessor.Infrastructure.Contracts.GetUser;

public record GetUserResponseDto(
    long Id,
    UserStatusDto Status,
    string PhoneNumber,
    UserPassportDto Passport);