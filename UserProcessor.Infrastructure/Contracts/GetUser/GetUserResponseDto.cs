using SharedKernel.Enums;
using UserProcessor.Infrastructure.Contracts.Models;

namespace UserProcessor.Infrastructure.Contracts.GetUser;

public record GetUserResponseDto(
    Ulid Id,
    UserStatus Status,
    string PhoneNumber,
    UserPassportDto? Passport);