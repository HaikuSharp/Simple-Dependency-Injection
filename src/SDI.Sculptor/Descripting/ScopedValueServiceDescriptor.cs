using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

/// <summary>
/// Descriptor for a service with scoped lifetime.
/// The same instance is returned within the same scope, but different scopes get different instances.
/// </summary>
public readonly struct ScopedValueServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : ILazyValueServiceDescriptor<ScopedValueServiceDescriptor>
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType;

    /// <inheritdoc/>
    public object Key => key;

    /// <inheritdoc/>
    public IServiceInstanceActivator Activator => activator;

    /// <inheritdoc/>
    public IServiceAccessor CreateAccessor() => new ScopedServiceAccessor(ServiceId.FromDescriptor(this), Activator);

    /// <inheritdoc/>
    public ScopedValueServiceDescriptor WithId(Type type, object key) => new(type, key, Activator);

    /// <inheritdoc/>
    public ScopedValueServiceDescriptor WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key, activator);
}