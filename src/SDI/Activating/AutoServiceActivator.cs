using SDI.Abstraction;
using SDI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

public sealed class AutoServiceActivator(Type serviceImplementationType) : IServiceInstanceActivator
{
    private readonly Dictionary<Type, IServiceInstanceActivator> m_Activators = [];

    public object Activate(ServiceId requestedId, IServiceProvider provider) => GetOrCreateActivator(requestedId, provider).Activate(requestedId, provider);

    private IServiceInstanceActivator GetOrCreateActivator(Type type, IServiceProvider provider)
    {
        if(m_Activators.TryGetValue(type, out var activator)) return activator;
        activator = provider.GetRequiredService<IServiceConstructorResolver>().Resolve(type.IsGenericType ? ConstructConcreteType(type, serviceImplementationType) : serviceImplementationType).AsActivator(); // provider.GetRequiredService<IServiceConstructorResolver>()
        m_Activators.Add(type, activator);
        return activator;
    }

    private static Type ConstructConcreteType(Type sourceType, Type genericDefinition)
    {
        var sourceTypeArgs = sourceType.GetGenericArguments();
        int targetGenericParamCount = genericDefinition.GetGenericArguments().Length;
        if(targetGenericParamCount > sourceTypeArgs.Length) throw new ArgumentException($"Not enough type arguments in {sourceType.Name} to construct {genericDefinition.Name}. Required: {targetGenericParamCount}, available: {sourceTypeArgs.Length}.", nameof(sourceType));
        Type[] typeArgsToUse = [.. sourceTypeArgs.Take(targetGenericParamCount)];
        return genericDefinition.MakeGenericType(typeArgsToUse);
    }
}