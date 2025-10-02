using SDI.Abstraction;
using SDI.Accessing;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

/// <summary>
/// Descriptor for a service with weak singleton lifetime.
/// The same instance is returned for every resolution request, but it can be garbage collected when no longer referenced.
/// </summary>
public readonly struct WeakSingletonValueServiceDescriptor(Type serviceType, object key, object instance) : IInstanceValueServiceDescriptor<WeakSingletonValueServiceDescriptor>
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType ?? instance?.GetType();

    /// <inheritdoc/>
    public object Key => key;

    /// <inheritdoc/>
    public object Instance => instance;

    /// <inheritdoc/>
    public IServiceAccessor CreateAccessor() => new WeakSingletonServiceAccessor(ServiceId.FromDescriptor(this), instance);

    /// <inheritdoc/>
    public WeakSingletonValueServiceDescriptor WithId(Type type, object key) => new(type, key, Instance);

    /// <inheritdoc/>
    public WeakSingletonValueServiceDescriptor WithInstance(object instance) => new(ServiceType, Key, instance);
}