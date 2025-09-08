using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using System;

namespace SDI.Descripting.Lazy;

/// <summary>
/// Descriptor for a lazy-initialized singleton service that creates and caches the instance on first access.
/// </summary>
public sealed class LazySingletonServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : LazyServiceDescriptorBase(serviceType, key, activator)
{
    /// <inheritdoc/>
    protected override IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator) => new LazySingletonServiceAccessor(id, activator);
}
