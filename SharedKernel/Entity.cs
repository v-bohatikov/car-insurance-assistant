namespace SharedKernel;

public class Entity
{
    protected Entity(Ulid id)
    {
        Id = id;
    }

    public Ulid Id { get; private set; }
}