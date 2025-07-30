namespace UserProcessor.Contracts.Models;

public record UserPassport(
    string Surname,
    string GivenNames,
    string PassportNumber,
    string Sex,
    DateOnly DateOfBirth);
