using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

public class ServiceInstanceAlreadyAddedException(ServiceId id) : Exception($"Service with [id: {id}] is already added.")
{
    public static void ThrowIfContains(IServiceInstanceContainer contanier, ServiceId id)
    {
        if(!contanier.HasInstance(id)) return;
        throw new ServiceInstanceAlreadyAddedException(id);
    }
}
