using SDI.Abstraction;
using System;

namespace SDI.Descripting.Lazy;

public abstract class LazyServiceDescriptorBase(Type serviceType, object key, IServiceInstanceActivator activator) : ServiceDescriptorBase(serviceType, key)
{
    public IServiceInstanceActivator Activator => activator;

    protected override IServiceAccessor CreateAccessor(ServiceId id) => CreateAccessor(id, activator);

    protected abstract IServiceAccessor CreateAccessor(ServiceId id, IServiceInstanceActivator activator);
}
