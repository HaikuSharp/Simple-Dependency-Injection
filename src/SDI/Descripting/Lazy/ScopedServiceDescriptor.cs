using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using System;

namespace SDI.Descripting.Lazy;

/// <summary>
/// Descriptor for a scoped service that creates one instance per service scope.
/// </summary>
public class ScopedServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : LazyServiceDescriptorBase(serviceType, key, activator)
{
    /// <inheritdoc/>
    protected override IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator) => new ScopedServiceAccessor(id, activator);
}
