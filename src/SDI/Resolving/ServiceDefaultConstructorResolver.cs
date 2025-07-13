using SDI.Abstraction;
using SDI.Activating.Constructing;
using SDI.Exceptions;
using System;
using System.Linq;

namespace SDI.Resolving;

public class ServiceDefaultConstructorResolver : IServiceConstructorResolver
{
    public static ServiceDefaultConstructorResolver Default => field ??= new();

    public IServiceConstructor Resolve(Type serviceImplementationType) => new ServiceConstructor(DefaultServiceConstructorNotFoundException.ThrowIfNull(serviceImplementationType.GetConstructors().OrderBy(c => c.GetParameters().Length).FirstOrDefault(), serviceImplementationType));
}