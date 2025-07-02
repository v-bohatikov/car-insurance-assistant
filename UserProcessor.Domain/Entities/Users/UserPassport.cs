using SharedKernel;

namespace UserProcessor.Domain.Entities.Users;

public sealed class UserPassport : Entity
{
    private UserPassport(
        long id,
        long userId,
        Guid fileId,
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

    public long UserId { get; set; }

    public Guid FileId { get; set; }

    public string Surname { get; set; }

    public string GivenNames { get; set; }

    public string PassportNumber { get; set; }

    public string Sex { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string FullName => string.Format($"{Surname} {GivenNames}");
}