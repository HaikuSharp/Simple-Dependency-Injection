using SDI.Abstraction;
using SDI.Accessing;
using System;

namespace SDI.Descripting;

public sealed class WeakSingletonServiceDescriptor(Type serviceType, object key, object instance) : SingletonServiceDescriptorBase(serviceType, key, instance)
{
    protected override IServiceAccessor CreateAccessor(ServiceId id, object instance) => new WeakSingletonServiceAccessor(id, instance);
}