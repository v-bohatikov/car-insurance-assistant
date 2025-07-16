namespace ApiGateway.Infrastructure.Contracts.Models;

public record UserPassportDto(
    string Surname,
    string GivenNames,
    string PassportNumber,
    string Sex,
    DateOnly DateOfBirth);