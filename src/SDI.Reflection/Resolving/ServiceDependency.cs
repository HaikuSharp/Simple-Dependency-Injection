using SDI.Abstraction;
using SDI.Reflection.Abstraction;
using SDI.Reflection.Extensions;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Reflection.Resolving;

/// <summary>
/// Represents a dependency that can resolve either a single service or a collection of services.
/// </summary>
public class ServiceDependency(Type serviceType, object key) : IServiceDependency
{
    /// <inheritdoc/>
    public object Resolve(IServiceProvider provider)
    {
        ServiceId id = ServiceId.FromType(serviceType);
        if(provider.IsImplemented(id)) return provider.GetService(id);
        var type = InternalEnumerableExtensions.GetElementType(serviceType);
        return type is not null ? provider.GetServices(ServiceId.FromType(type, key)).ConvertToArray(type) : null;
    }
}
