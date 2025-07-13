using SDI.Abstraction;
using SDI.Extensions;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

public abstract class ConstructorServiceActivatorBase : IServiceInstanceActivator
{
    private IServiceDependency[] m_Dependencies;
    private object[] m_ArgumentsBuffer;

    public object Activate(ServiceId requestedId, IServiceProvider provider)
    {
        var constructor = GetConstructor(provider);
        object[] buffer = GetOrCreateArgumentsBuffer(provider, GetOrResolveDependencies(provider, constructor));
        object instance = constructor.Invoke(buffer);
        Array.Clear(buffer, 0, buffer.Length);
        return instance;
    }

    protected abstract IServiceConstructor GetConstructor(IServiceProvider provider);

    private object[] GetOrCreateArgumentsBuffer(IServiceProvider provider, IServiceDependency[] dependencies)
    {
        object[] buffer = m_ArgumentsBuffer ??= new object[dependencies.Length];

        if(buffer.Length is 0) return buffer;

        for(int i = 0; i < buffer.Length; i++) buffer[i] = dependencies[i].Resolve(provider);

        return buffer;
    }

    private IServiceDependency[] GetOrResolveDependencies(IServiceProvider provider, IServiceConstructor constructor)
    {
        var dependencies = m_Dependencies;

        if(dependencies is not null) return m_Dependencies;

        var parameters = constructor.Parameters;

        if(parameters.Count is 0) return m_Dependencies = [];

        var dependencyResolver = provider.GetRequiredService<IServiceDependencyResolver>();
        dependencies = m_Dependencies = new IServiceDependency[parameters.Count];

        for(int i = 0; i < dependencies.Length; i++) dependencies[i] = dependencyResolver.Resolve(ServiceDependencyInfo.FromParameter(parameters[i]));

        return dependencies;
    }
}
