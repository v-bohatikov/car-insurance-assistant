using ApiGateway.Infrastructure.Contracts.Models;
using SharedKernel.Enums;

namespace ApiGateway.Infrastructure.Contracts.GetUser;

public record GetUserResponseDto(
    long Id,
    UserStatus Status,
    string PhoneNumber,
    UserPassportDto? Passport);