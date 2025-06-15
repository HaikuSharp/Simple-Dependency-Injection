using SDI.Abstraction;
using System;

namespace SDI.Dependencies;

public abstract class ServiceDependencyBase(Type serviceType, object key) : IServiceDependency
{
    public Type ServiceType => serviceType;

    public object Key => key;

    public object GetDependency(Abstraction.IServiceProvider provider) => GetDependency(provider, ServiceId.FromType(serviceType, key));

    public abstract object GetDependency(Abstraction.IServiceProvider provider, ServiceId id);
}
