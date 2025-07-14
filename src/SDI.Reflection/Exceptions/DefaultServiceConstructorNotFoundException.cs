using System;
using System.Reflection;

namespace SDI.Reflection.Exceptions;

public class DefaultServiceConstructorNotFoundException(Type serviceImplementationType) : Exception($"Service implementation [{serviceImplementationType.FullName}] default constructor not found.")
{
    public static TConstructor ThrowIfNull<TConstructor>(TConstructor constructor, Type type) where TConstructor : MethodBase => constructor is not null ? constructor : throw new DefaultServiceConstructorNotFoundException(type);
}
