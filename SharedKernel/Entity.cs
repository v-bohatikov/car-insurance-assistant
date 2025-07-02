namespace SharedKernel;

public class Entity
{
    protected Entity(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}