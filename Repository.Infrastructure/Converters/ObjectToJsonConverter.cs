using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Repository.Infrastructure.Converters;

public class ObjectToJsonConverter<T>
    : ValueConverter<T, string>
    where T : class
{
    public ObjectToJsonConverter()
        : this(null)
    { }

    public ObjectToJsonConverter(ConverterMappingHints? mappingHints)
        : base(
            convertToProviderExpression: objectValue => Serialize(objectValue),
            convertFromProviderExpression: stringValue => Deserialize(stringValue),
            mappingHints: mappingHints)
    { }

    private static JsonSerializerSettings JsonSerializerSettings => new()
    {
        TypeNameHandling = TypeNameHandling.Objects
    };

    private static string Serialize(T objectValue)
    {
        var serialized = JsonConvert
            .SerializeObject(objectValue, objectValue.GetType(), JsonSerializerSettings);

        return serialized;
    }

    private static T Deserialize(string stringValue)
    {
        var deserialized = JsonConvert
            .DeserializeObject<T>(stringValue, JsonSerializerSettings)!;

        return deserialized;
    }
}