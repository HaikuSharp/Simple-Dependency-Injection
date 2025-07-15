using SDI.Abstraction;
using SDI.Accessing.Lazy;
using System;

namespace SDI.Descripting.Lazy;

/// <summary>
/// Descriptor for a transient service that creates a new instance for each request.
/// </summary>
public class TransientServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : LazyServiceDescriptorBase(serviceType, key, activator)
{
    /// <inheritdoc/>
    protected override IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator) => new TransientServiceAccessor(id, activator);
}