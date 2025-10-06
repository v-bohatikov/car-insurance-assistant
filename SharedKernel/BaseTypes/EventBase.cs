namespace SharedKernel.BaseTypes;

public record EventBase
{
    private const string NoUserId = "01K3HGFQ400000000000000000";

    public static Ulid NoUserUlid => Ulid.Parse(NoUserId);

    public EventBase(Ulid userId)
    {
        Id = Ulid.NewUlid();
        UserId = userId;
    }

    public Ulid Id { get; private set; }

    public Ulid UserId { get; private set; }
}