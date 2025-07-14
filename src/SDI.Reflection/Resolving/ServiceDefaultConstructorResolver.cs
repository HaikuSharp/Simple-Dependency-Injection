using SDI.Abstraction;
using SDI.Exceptions;
using SDI.Reflection.Activating.Constructing;
using System;
using System.Linq;

namespace SDI.Reflection.Resolving;

public class ServiceDefaultConstructorResolver : IServiceConstructorResolver
{
    public static ServiceDefaultConstructorResolver Default => field ??= new();

    public IServiceConstructor Resolve(Type serviceImplementationType) => new ServiceConstructor(DefaultServiceConstructorNotFoundException.ThrowIfNull(serviceImplementationType.GetConstructors().OrderBy(c => c.GetParameters().Length).FirstOrDefault(), serviceImplementationType));
}