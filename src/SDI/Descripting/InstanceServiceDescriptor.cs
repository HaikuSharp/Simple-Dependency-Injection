using SDI.Abstraction;
using SDI.Accessing;
using System;

namespace SDI.Descripting;

public sealed class InstanceServiceDescriptor(Type serviceType, object key, object instance) : InstanceServiceDescriptorBase(serviceType, key, instance)
{
    protected override IServiceAccessor CreateAccessor(ServiceId id, object instance) => new InstanceServiceAccessor(id, instance);
}
