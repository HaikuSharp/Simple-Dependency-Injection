using SDI.Abstraction;
using SDI.Accessing;
using System;

namespace SDI.Descripting;

public class LazyInstanceServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : InstantiateServiceDescriptorBase(serviceType, key, activator)
{
    protected override IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator) => new LazyInstanceServiceAccessor(id, activator);
}
