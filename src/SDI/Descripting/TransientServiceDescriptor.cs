using SDI.Abstraction;
using SDI.Accessing;
using System;

namespace SDI.Descripting;

public sealed class TransientServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : InstantiateServiceDescriptorBase(serviceType, key, activator)
{
    protected override IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator) => new TransientServiceAccessor(id, activator);
}
