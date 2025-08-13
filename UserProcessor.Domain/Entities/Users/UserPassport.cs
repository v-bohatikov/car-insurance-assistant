using SharedKernel;

namespace UserProcessor.Domain.Entities.Users;

public sealed class UserPassport : Entity
{
    private UserPassport(
        Ulid id,
        Ulid userId,
        Ulid fileId,
        string surname,
        string givenNames,
        string passportNumber,
        string sex,
        DateOnly dateOfBirth)
        : base(id)
    {
        UserId = userId;
        FileId = fileId;
        Surname = surname;
        GivenNames = givenNames;
        PassportNumber = passportNumber;
        Sex = sex;
        DateOfBirth = dateOfBirth;
    }

    public Ulid UserId { get; set; }

    public Ulid FileId { get; set; }

    public string Surname { get; set; }

    public string GivenNames { get; set; }

    public string PassportNumber { get; set; }

    public string Sex { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string FullName => string.Format($"{Surname} {GivenNames}");
}