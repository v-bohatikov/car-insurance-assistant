using System.Collections;

namespace SharedKernel.Extensions;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<TCollection>(this TCollection? collection)
        where TCollection : class, ICollection
    {
        if (ReferenceEquals(collection, null))
        {
            return true;
        }

        return collection.Count != 0;
    }
}