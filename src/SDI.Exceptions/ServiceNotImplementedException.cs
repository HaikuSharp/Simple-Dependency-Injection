using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

public class ServiceNotImplementedException(ServiceId id) : Exception($"Service with [id: {id}] is not implemented.")
{
    public static void ThrowIfNotImplemented(Abstraction.IServiceProvider provider, ServiceId id)
    {
        if(provider.IsImplemented(id)) return;
        throw new ServiceNotImplementedException(id);
    }
}
