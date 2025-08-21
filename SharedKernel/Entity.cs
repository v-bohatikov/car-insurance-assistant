namespace SharedKernel;

public class Entity
{
    protected Entity(Ulid id)
    {
        Id = id;
    }

    public Ulid Id { get; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ChangedOn { get; set; }
}