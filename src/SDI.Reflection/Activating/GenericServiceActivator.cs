using SDI.Abstraction;
using SDI.Reflection.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDI.Reflection.Activating;

/// <summary>
/// Service activator that handles generic type instantiation by constructing concrete types
/// from generic type definitions and caching the resulting activators.
/// </summary>
public sealed class GenericServiceActivator(Type serviceImplementationType, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) : IServiceInstanceActivator
{
    private readonly Dictionary<Type, IServiceInstanceActivator> m_Activators = [];

    /// <inheritdoc/>
    public object Activate(ServiceId requestedId, SDI.Abstraction.IServiceProvider provider) => GetOrCreateActivator(requestedId.Type).Activate(requestedId, provider);

    private IServiceInstanceActivator GetOrCreateActivator(Type type)
    {
#if NETCOREAPP
        ArgumentNullException.ThrowIfNull(type);
#else
        if(type is null) throw new ArgumentNullException(nameof(type));
#endif
        if(m_Activators.TryGetValue(type, out var activator)) return activator;
        var concreteType = type.IsGenericType ? ConstructConcreteType(type, serviceImplementationType) : serviceImplementationType;
        if(!type.IsAssignableFrom(concreteType)) throw new ArgumentException($"Type {concreteType.FullName} is not compatible with requested type {type.FullName}");
        m_Activators.Add(type, activator = new DefaultConstructorServiceActivator(concreteType, constructorResolver, dependencyResolver));
        return activator;
    }

    private static Type ConstructConcreteType(Type sourceType, Type genericDefinition)
    {
        var sourceTypeArgs = sourceType.GetGenericArguments();
        int targetGenericParamCount = genericDefinition.GetGenericArguments().Length;
        return targetGenericParamCount > sourceTypeArgs.Length
            ? throw new ArgumentException($"Not enough type arguments in {sourceType.FullName} to construct {genericDefinition.FullName}. Required: {targetGenericParamCount}, available: {sourceTypeArgs.Length}.", nameof(sourceType))
            : genericDefinition.MakeGenericType([.. sourceTypeArgs.Take(targetGenericParamCount)]);
    }
}
