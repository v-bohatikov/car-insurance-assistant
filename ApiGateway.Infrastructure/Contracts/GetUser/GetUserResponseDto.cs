using ApiGateway.Infrastructure.Contracts.Models;
using SharedKernel.Enums;

namespace ApiGateway.Infrastructure.Contracts.GetUser;

public record GetUserResponseDto(
    Ulid Id,
    UserStatus Status,
    string PhoneNumber,
    UserPassportDto? Passport);