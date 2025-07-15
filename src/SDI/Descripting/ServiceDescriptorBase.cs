using SDI.Abstraction;
using System;

namespace SDI.Descripting;

/// <summary>
/// Base class for service descriptors that provides common functionality for service registration.
/// </summary>
public abstract class ServiceDescriptorBase(Type serviceType, object key) : IServiceDescriptor
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType;

    /// <inheritdoc/>
    public object Key => key;

    /// <inheritdoc/>
    public IServiceAccessor CreateAccessor() => CreateAccessor(ServiceId.FromDescriptor(this));

    /// <summary>
    /// When implemented in a derived class, creates a service accessor for the specified service ID.
    /// </summary>
    /// <param name="id">The service identifier to create an accessor for.</param>
    /// <returns>
    /// An <see cref="IServiceAccessor"/> implementation specific to the service lifetime strategy.
    /// </returns>
    protected abstract IServiceAccessor CreateAccessor(ServiceId id);
}