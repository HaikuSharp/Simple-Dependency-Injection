using SDI.Abstraction;
using SDI.Accessing;
using System;

namespace SDI.Descripting;

/// <summary>
/// Descriptor for a weakly-referenced singleton service instance that allows garbage collection when no other references exist.
/// </summary>
public sealed class WeakSingletonServiceDescriptor(Type serviceType, object key, object instance) : SingletonServiceDescriptorBase(serviceType, key, instance)
{
    /// <inheritdoc/>
    protected override IServiceAccessor CreateAccessor(ServiceId id, object instance) => new WeakSingletonServiceAccessor(id, instance);
}