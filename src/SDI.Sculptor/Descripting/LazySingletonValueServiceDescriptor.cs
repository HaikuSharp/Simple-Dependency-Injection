using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

/// <summary>
/// Descriptor for a service with lazy singleton lifetime.
/// The instance is created on first request and then reused for subsequent requests.
/// </summary>
public readonly struct LazySingletonValueServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : ILazyValueServiceDescriptor<LazySingletonValueServiceDescriptor>
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType;

    /// <inheritdoc/>
    public object Key => key;

    /// <summary>
    /// Gets the activator used to create instances of the service.
    /// </summary>
    public IServiceInstanceActivator Activator => activator;

    /// <inheritdoc/>
    public IServiceAccessor CreateAccessor() => new LazySingletonServiceAccessor(ServiceId.FromDescriptor(this), Activator);

    /// <inheritdoc/>
    public LazySingletonValueServiceDescriptor WithId(Type type, object key) => new(type, key, Activator);

    /// <inheritdoc/>
    public LazySingletonValueServiceDescriptor WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key, activator);
}