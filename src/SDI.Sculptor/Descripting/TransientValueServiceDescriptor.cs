using SDI.Abstraction;
using SDI.Accessing.Lazy;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

/// <summary>
/// Descriptor for a service with transient lifetime.
/// A new instance is created every time the service is resolved.
/// </summary>
public readonly struct TransientValueServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : ILazyValueServiceDescriptor<TransientValueServiceDescriptor>
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType;

    /// <inheritdoc/>
    public object Key => key;

    /// <inheritdoc/>
    public IServiceInstanceActivator Activator => activator;

    /// <inheritdoc/>
    public IServiceAccessor CreateAccessor() => new TransientServiceAccessor(ServiceId.FromDescriptor(this), Activator);

    /// <inheritdoc/>
    public TransientValueServiceDescriptor WithId(Type type, object key) => new(type, key, Activator);

    /// <inheritdoc/>
    public TransientValueServiceDescriptor WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key, activator);
}