using SDI.Abstraction;
using System.Collections.Generic;
namespace SDI.Activators;

public class ServiceConstructorInstanceActivator(IServiceConstructorInvoker invoker, IEnumerable<IServiceDependency> dependencies) : IServiceInstanceActivator
{
    private object[] m_ArgumentsBuffer;
    private readonly IServiceDependency[] m_Dependencies = [.. dependencies];

    public object Activate(IServiceProvider provider) => invoker.Invoke(GetOrCreateArgumentsBuffer(provider));

    private object[] GetOrCreateArgumentsBuffer(IServiceProvider provider)
    {
        var dependencies = m_Dependencies;
        object[] buffer = m_ArgumentsBuffer ??= new object[dependencies.Length];
        for(int i = 0; i < buffer.Length; i++) buffer[i] = dependencies[i].GetDependency(provider);
        return buffer;
    }
}
