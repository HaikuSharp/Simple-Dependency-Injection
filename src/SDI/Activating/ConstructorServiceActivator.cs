using SDI.Abstraction;
using System;
using System.Reflection;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

public sealed class ConstructorServiceActivator(IServiceConstructor constructor) : EmptyConstructorServiceActivatorBase(constructor)
{
    private IServiceDependency[] m_Dependencies;
    private object[] m_ArgumentsBuffer;

    protected override object Activate(IServiceProvider provider, IServiceConstructor constructor)
    {
        var buffer = GetOrCreateArgumentsBuffer(provider, GetOrResolveDependencies(provider, constructor));
        var instance = constructor.Invoke(buffer);
        Array.Clear(buffer, 0, buffer.Length);
        return instance;
    }

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

        var dependencyResolver = provider.GetService(ServiceId.From<IServiceDependencyResolver<ParameterInfo>>()) as IServiceDependencyResolver<ParameterInfo>;

        var parameters = constructor.Parameters;
        dependencies = m_Dependencies = new IServiceDependency[parameters.Count];

        for(int i = 0; i < dependencies.Length; i++) dependencies[i] = dependencyResolver.Resolve(parameters[i]);

        return dependencies;
    }
}
