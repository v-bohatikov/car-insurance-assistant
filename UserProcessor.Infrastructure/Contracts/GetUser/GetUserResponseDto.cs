using SharedKernel.Enums;
using UserProcessor.Infrastructure.Contracts.Models;

namespace UserProcessor.Infrastructure.Contracts.GetUser;

public record GetUserResponseDto(
    long Id,
    UserStatus Status,
    string PhoneNumber,
    UserPassportDto? Passport);