using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repository.Infrastructure.Converters;

public class TypeToStringConverter : ValueConverter<Type, string>
{
    public TypeToStringConverter()
        : this(null)
    { }

    public TypeToStringConverter(ConverterMappingHints? mappingHints)
        : base(
            convertToProviderExpression: typeValue => typeValue.AssemblyQualifiedName!,
            convertFromProviderExpression: stringValue => Type.GetType(stringValue)!,
            mappingHints: mappingHints)
    { }
}