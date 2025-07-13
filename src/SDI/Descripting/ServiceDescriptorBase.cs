using SDI.Abstraction;
using System;

namespace SDI.Descripting;

public abstract class ServiceDescriptorBase(Type serviceType, object key) : IServiceDescriptor
{
    public Type ServiceType => serviceType;

    public object Key => key;

    public IServiceAccessor CreateAccessor() => CreateAccessor(ServiceId.FromDescriptor(this));

    protected abstract IServiceAccessor CreateAccessor(ServiceId id);
}
