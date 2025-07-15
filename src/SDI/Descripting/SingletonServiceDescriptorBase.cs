using SDI.Abstraction;
using System;

namespace SDI.Descripting;

/// <summary>
/// Base class for service descriptors that manage singleton service instances.
/// </summary>
public abstract class SingletonServiceDescriptorBase(Type serviceType, object key, object instance) : ServiceDescriptorBase(serviceType, key)
{
    /// <summary>
    /// Gets the singleton instance that this descriptor manages.
    /// </summary>
    public object Instance => instance;

    /// <inheritdoc/>
    protected sealed override IServiceAccessor CreateAccessor(ServiceId id) => CreateAccessor(id, instance);

    /// <summary>
    /// When implemented in a derived class, creates a service accessor for the specified singleton instance.
    /// </summary>
    /// <param name="id">The service identifier.</param>
    /// <param name="instance">The singleton instance to provide access to.</param>
    /// <returns>
    /// An <see cref="IServiceAccessor"/> implementation specific to the singleton lifetime strategy.
    /// </returns>
    protected abstract IServiceAccessor CreateAccessor(ServiceId id, object instance);
}