using SDI.Abstraction;
using SDI.Extensions;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

public sealed class DefaultConstructorServiceActivator(Type serviceImplementationType) : ConstructorServiceActivatorBase
{
    private IServiceConstructor m_Constructor;

    protected override IServiceConstructor GetConstructor(IServiceProvider provider) => m_Constructor ??= provider.GetRequiredService<IServiceConstructorResolver>().Resolve(serviceImplementationType);
}
