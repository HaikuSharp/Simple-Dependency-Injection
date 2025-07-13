using SDI.Abstraction;
using SDI.Accessing;
using System;

namespace SDI.Descripting.Lazy;

public class TransientServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : LazyServiceDescriptorBase(serviceType, key, activator)
{
    protected override IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator) => new TransientServiceAccessor(id, activator);
}