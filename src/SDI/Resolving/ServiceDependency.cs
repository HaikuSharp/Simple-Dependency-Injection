using SDI.Abstraction;
using SDI.Extensions;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Resolving;

public class ServiceDependency(Type serviceType, object key) : IServiceDependency
{
    public object Resolve(IServiceProvider provider)
    {
        ServiceId id = ServiceId.FromType(serviceType);
        if(provider.IsImplemented(id)) return provider.GetService(id);
        var type = serviceType.GetElementType();
        return type is not null ? provider.GetServices(ServiceId.FromType(type, key)).ConvertToArray(type) : null;
    }
}
