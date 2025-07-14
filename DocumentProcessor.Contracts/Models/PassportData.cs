namespace DocumentProcessor.Contracts.Models;

public record PassportData(
    string Surname,
    string GivenNames,
    string PassportNumber,
    string Sex,
    DateOnly DateOfBirth);