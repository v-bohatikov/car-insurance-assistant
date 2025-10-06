using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repository.Infrastructure.Converters;

public class UlidToStringConverter : ValueConverter<Ulid, string>
{
    private static readonly ConverterMappingHints DefaultHints = new ConverterMappingHints(size: 26);

    public UlidToStringConverter()
        : this(null)
    { }

    public UlidToStringConverter(ConverterMappingHints? mappingHints)
        : base(
            convertToProviderExpression: ulidValue => ulidValue.ToString(),
            convertFromProviderExpression: stringValue => Ulid.Parse(stringValue),
            mappingHints: DefaultHints.With(mappingHints))
    { }
}