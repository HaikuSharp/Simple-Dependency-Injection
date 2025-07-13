using SDI.Abstraction;
using System;

namespace SDI.Descripting;

public abstract class InstantiateServiceDescriptorBase(Type serviceType, object key, IServiceInstanceActivator activator) : ServiceDescriptorBase(serviceType, key)
{
    public IServiceInstanceActivator Activator => activator;

    protected sealed override IServiceAccessor CreateAccessor(ServiceId id) => CreateAccessor(id, activator);

    protected abstract IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator);
}
