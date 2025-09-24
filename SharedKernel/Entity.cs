namespace SharedKernel;

public abstract class Entity
{
    protected Entity(Ulid id)
    {
        Id = id;
    }

    public Ulid Id { get; init; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ChangedOn { get; set; }
}