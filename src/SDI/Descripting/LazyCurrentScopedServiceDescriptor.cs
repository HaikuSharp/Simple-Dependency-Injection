using SDI.Abstraction;
using SDI.Accessing;
using System;

namespace SDI.Descripting;

public class LazyCurrentScopedServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : InstantiateServiceDescriptorBase(serviceType, key, activator)
{
    protected override IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator) => new LazyCurrentScopedServiceAccessor(id, activator);
}