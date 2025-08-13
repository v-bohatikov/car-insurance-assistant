namespace UserProcessor.Infrastructure.Contracts.Models;

public record UserPassportDto(
    Ulid Id,
    Ulid UserId,
    Ulid FileId,
    string Surname,
    string GivenNames,
    string PassportNumber,
    string Sex,
    DateOnly DateOfBirth);