using SDI.Abstraction;
using SDI.Reflection.Extensions;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Reflection.Resolving;

public class ServiceDependency(Type serviceType, object key) : IServiceDependency
{
    public object Resolve(IServiceProvider provider)
    {
        ServiceId id = ServiceId.FromType(serviceType);
        if(provider.IsImplemented(id)) return provider.GetService(id);
        var type = InternalEnumerableExtensions.GetElementType(serviceType);
        return type is not null ? provider.GetServices(ServiceId.FromType(type, key)).ConvertToArray(type) : null;
    }
}
