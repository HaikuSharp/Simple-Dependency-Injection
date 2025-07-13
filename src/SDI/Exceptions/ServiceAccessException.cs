using SDI.Abstraction;
using System;
using System.Reflection;

namespace SDI.Exceptions;

public class ServiceAccessException(ServiceId id) : NullReferenceException($"Service [id: {id}] instance cant be null.")
{
    public static void ThrowIfServiceIntanceIsNull(ServiceId id, object instance)
    {
        if(instance is not null) return;
        throw new ServiceAccessException(id);
    }
}

public class DefaultServiceConstructorNotFoundException(Type serviceImplementationType) : Exception($"Service implementation [{serviceImplementationType.FullName}] default constructor not found.")
{
    public static TConstructor ThrowIfNull<TConstructor>(TConstructor constructor, Type type) where TConstructor : MethodBase => constructor is not null ? constructor : throw new DefaultServiceConstructorNotFoundException(type);
}
