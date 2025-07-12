using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

public class ServiceAlreadyRegisteredException(ServiceId id) : Exception($"Service with [id: {id}] is already registered.")
{
    public static void ThrowIfRegistered(IServiceController controller, ServiceId id)
    {
        if(!controller.IsRegistered(id)) return;
        throw new ServiceAlreadyRegisteredException(id);
    }
}
