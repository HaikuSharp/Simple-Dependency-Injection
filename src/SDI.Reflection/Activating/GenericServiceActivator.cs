using SDI.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Reflection.Activating;

public sealed class GenericServiceActivator(Type serviceImplementationType) : IServiceInstanceActivator
{
    private readonly Dictionary<Type, IServiceInstanceActivator> m_Activators = [];

    public object Activate(ServiceId requestedId, IServiceProvider provider) => GetOrCreateActivator(requestedId).Activate(requestedId, provider);

    private IServiceInstanceActivator GetOrCreateActivator(Type type)
    {
        if(m_Activators.TryGetValue(type, out var activator)) return activator;
        var concreteType = type.IsGenericType ? ConstructConcreteType(type, serviceImplementationType) : serviceImplementationType;
        if(!type.IsAssignableFrom(concreteType)) throw new ArgumentException($"Type {concreteType.FullName} is not compatible with requested type {type.FullName}");
        activator = new DefaultConstructorServiceActivator(concreteType);
        m_Activators.Add(type, activator);
        return activator;
    }

    private static Type ConstructConcreteType(Type sourceType, Type genericDefinition)
    {
        var sourceTypeArgs = sourceType.GetGenericArguments();
        int targetGenericParamCount = genericDefinition.GetGenericArguments().Length;
        if(targetGenericParamCount > sourceTypeArgs.Length) throw new ArgumentException($"Not enough type arguments in {sourceType.FullName} to construct {genericDefinition.FullName}. Required: {targetGenericParamCount}, available: {sourceTypeArgs.Length}.", nameof(sourceType));
        Type[] typeArgsToUse = [.. sourceTypeArgs.Take(targetGenericParamCount)];
        return genericDefinition.MakeGenericType(typeArgsToUse);
    }
}
