namespace Repository.Infrastructure.Abstractions;

public interface IDefaultContainerProvider
{
    string GetDefaultContainerName();
}

public class DefaultContainerProvider : IDefaultContainerProvider
{
    private readonly string _defaultContainerName;

    private DefaultContainerProvider(string defaultContainerName)
    {
        _defaultContainerName = defaultContainerName;
    }

    public string GetDefaultContainerName() => _defaultContainerName;

    public static DefaultContainerProvider Create(string defaultContainerName) =>
        new (defaultContainerName);
}