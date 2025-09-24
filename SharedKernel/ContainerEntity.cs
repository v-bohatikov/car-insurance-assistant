namespace SharedKernel;

public abstract class ContainerEntity : Entity
{
    protected ContainerEntity(
        Ulid id,
        Ulid userId,
        string? discriminator = null)
        : base(id)
    {
        UserId = userId;
        Discriminator = discriminator;
    }

    public Ulid UserId { get; init; }

    public string? Discriminator { get; init; }
}