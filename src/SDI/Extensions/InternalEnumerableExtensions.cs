using Sugar.Object.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SDI.Extensions;

internal static class InternalEnumerableExtensions
{
    private static ArrayList s_Buffer;

    internal static Array ConvertToArray(this IEnumerable source, Type itemType)
    {
        var buffer = s_Buffer ??= [];
        foreach(object item in source) buffer.Add(item).Forget();
        Array array = Array.CreateInstance(itemType, buffer.Count);
        buffer.CopyTo(array);
        buffer.Clear();
        return array;
    }

    internal static Type GetElementType(this Type type)
    {
        if(type.IsArray) return type.GetElementType();
        if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)) return type.GetGenericArguments()[0];
        var enumerableInterface = type.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        return enumerableInterface is not null ? enumerableInterface.GetGenericArguments()[0] : typeof(IEnumerable).IsAssignableFrom(type) ? typeof(object) : null;
    }
}
