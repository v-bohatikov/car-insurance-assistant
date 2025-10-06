using SharedKernel;
using SharedKernel.Enums;

namespace UserProcessor.Domain.Entities.Users;

public sealed class UserPassport : Entity
{
    private UserPassport(
        Ulid id,
        Ulid userId,
        Ulid documentId,
        string surname,
        string givenNames,
        string passportNumber,
        Sex sex,
        DateOnly dateOfBirth)
        : base(id)
    {
        UserId = userId;
        DocumentId = documentId;
        Surname = surname;
        GivenNames = givenNames;
        PassportNumber = passportNumber;
        Sex = sex;
        DateOfBirth = dateOfBirth;
    }

    public Ulid UserId { get; }

    public Ulid DocumentId { get; }

    public string Surname { get; }

    public string GivenNames { get; }

    public string PassportNumber { get; }

    public Sex Sex { get; }

    public DateOnly DateOfBirth { get; }

    public string FullName => string.Format($"{Surname} {GivenNames}");
}