using SDI.Abstraction;
using SDI.Extensions;
using SDI.Reflection.Abstraction;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Reflection.Activating;

/// <summary>
/// Base class for service activators that use constructor injection to create service instances.
/// </summary>
public abstract class ConstructorServiceActivatorBase : IServiceInstanceActivator
{
    private IServiceDependency[] m_Dependencies;
    private object[] m_ArgumentsBuffer;

    /// <summary>
    /// Creates a service instance using constructor injection.
    /// </summary>
    /// <param name="requestedId">The service identifier being activated.</param>
    /// <param name="provider">The service provider for dependency resolution.</param>
    /// <returns>The newly created service instance.</returns>
    public object Activate(ServiceId requestedId, IServiceProvider provider)
    {
        var constructor = GetConstructor(provider);
        object[] buffer = constructor.Parameters.Count is 0 ? [] : GetOrCreateArgumentsBuffer(provider, GetOrResolveDependencies(provider, constructor));
        object instance = constructor.Invoke(buffer);
        Array.Clear(buffer, 0, buffer.Length);
        return instance;
    }

    /// <summary>
    /// When implemented in derived classes, gets the constructor to use for service activation.
    /// </summary>
    /// <param name="provider">The service provider context.</param>
    /// <returns>A <see cref="IServiceConstructor"/> representing the constructor to invoke.</returns>
    protected abstract IServiceConstructor GetConstructor(IServiceProvider provider);

    private object[] GetOrCreateArgumentsBuffer(IServiceProvider provider, IServiceDependency[] dependencies)
    {
        object[] buffer = m_ArgumentsBuffer ??= new object[dependencies.Length];

        for(int i = 0; i < buffer.Length; i++) buffer[i] = dependencies[i].Resolve(provider);

        return buffer;
    }

    private IServiceDependency[] GetOrResolveDependencies(IServiceProvider provider, IServiceConstructor constructor)
    {
        var dependencies = m_Dependencies;

        if(dependencies is not null) return m_Dependencies;

        var dependencyResolver = provider.GetRequiredService<IServiceDependencyResolver>();
        var parameters = constructor.Parameters;
        dependencies = m_Dependencies = new IServiceDependency[parameters.Count];

        for(int i = 0; i < dependencies.Length; i++) dependencies[i] = dependencyResolver.Resolve(ServiceDependencyInfo.FromParameter(parameters[i]));

        return dependencies;
    }
}
