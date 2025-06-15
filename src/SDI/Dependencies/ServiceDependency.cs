using SDI.Abstraction;
using SDI.Helpers;
using System;

namespace SDI.Dependencies;

public class ServiceDependency(Type serviceType, object key) : ServiceDependencyBase(serviceType, key)
{
    public override object GetDependency(Abstraction.IServiceProvider provider, ServiceId id)
    {
        if(provider.IsImplemented(id)) return provider.GetService(id);
        Type serviceType = EnumerableHelper.GetElementType(ServiceType);
        return serviceType is not null ? provider.GetServices(ServiceId.FromType(serviceType, Key)).ConvertToArray(serviceType) : (object)null;
    }
}