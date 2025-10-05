using SDI.Extensions;
using SDI.Reflection.Abstraction;
using System;

namespace SDI.Reflection.Activating;

/// <summary>
/// Service activator that resolves and uses the default constructor (with fewest parameters)
/// for service instance creation.
/// </summary>
public sealed class DefaultConstructorServiceActivator(Type serviceImplementationType) : ConstructorServiceActivatorBase
{
    private IServiceConstructor m_Constructor;

    /// <inheritdoc/>
    protected override IServiceConstructor GetConstructor(SDI.Abstraction.IServiceProvider provider) => m_Constructor ??= provider.GetRequiredService<IServiceConstructorResolver>().Resolve(serviceImplementationType);
}
