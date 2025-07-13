using SDI.Abstraction;
using SDI.Accessing;
using System;

namespace SDI.Descripting;

public abstract class SingletonServiceDescriptorBase(Type serviceType, object key, object instance) : ServiceDescriptorBase(serviceType, key)
{
    public object Isntance => instance;

    protected sealed override IServiceAccessor CreateAccessor(ServiceId id) => CreateAccessor(id, instance);

    protected abstract IServiceAccessor CreateAccessor(ServiceId id, object instance);
}
