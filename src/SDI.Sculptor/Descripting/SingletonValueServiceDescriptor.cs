using SDI.Abstraction;
using SDI.Accessing;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

/// <summary>
/// Descriptor for a service with singleton lifetime.
/// The same instance is returned for every resolution request.
/// </summary>
public readonly struct SingletonValueServiceDescriptor(Type serviceType, object key, object instance) : IInstanceValueServiceDescriptor<SingletonValueServiceDescriptor>
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType ?? instance?.GetType();

    /// <inheritdoc/>
    public object Key => key;

    /// <inheritdoc/>
    public object Instance => instance;

    /// <inheritdoc/>
    public IServiceAccessor CreateAccessor() => new SingletonServiceAccessor(ServiceId.FromDescriptor(this), instance);

    /// <inheritdoc/>
    public SingletonValueServiceDescriptor WithId(Type type, object key) => new(type, key, Instance);

    /// <inheritdoc/>
    public SingletonValueServiceDescriptor WithInstance(object instance) => new(ServiceType, Key, instance);
}