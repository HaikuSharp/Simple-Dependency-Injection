using SDI.Abstraction;
using SDI.Accessing;
using System;

namespace SDI.Descripting;

/// <summary>
/// Descriptor for a strongly-referenced singleton service instance.
/// </summary>
public sealed class SingletonServiceDescriptor(Type serviceType, object key, object instance) : SingletonServiceDescriptorBase(serviceType, key, instance)
{
    /// <inheritdoc/>
    protected override IServiceAccessor CreateAccessor(ServiceId id, object instance) => new SingletonServiceAccessor(id, instance);
}
