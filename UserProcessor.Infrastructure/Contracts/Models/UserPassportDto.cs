namespace UserProcessor.Infrastructure.Contracts.Models;

public record UserPassportDto(
    long Id,
    long UserId,
    Guid FileId,
    string Surname,
    string GivenNames,
    string PassportNumber,
    string Sex,
    DateOnly DateOfBirth);