using SDI.Abstraction;
using System;

namespace SDI.Descripting.Lazy;

/// <summary>
/// Base class for service descriptors that support lazy initialization of service instances.
/// </summary>
public abstract class LazyServiceDescriptorBase(Type serviceType, object key, IServiceInstanceActivator activator) : ServiceDescriptorBase(serviceType, key)
{
    /// <summary>
    /// Gets the activator responsible for creating service instances when they are requested.
    /// </summary>
    public IServiceInstanceActivator Activator => activator;

    /// <inheritdoc/>
    protected override IServiceAccessor CreateAccessor(ServiceId id) => CreateAccessor(id, activator);

    /// <summary>
    /// When implemented in a derived class, creates a lazy-initialized service accessor 
    /// using the specified activator.
    /// </summary>
    /// <param name="id">The service identifier.</param>
    /// <param name="activator">The activator to use for instance creation.</param>
    /// <returns>
    /// An <see cref="IServiceAccessor"/> implementation specific to the lazy initialization strategy.
    /// </returns>
    protected abstract IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator);
}