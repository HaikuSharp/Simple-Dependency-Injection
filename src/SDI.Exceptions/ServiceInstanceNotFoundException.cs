using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

public class ServiceInstanceNotFoundException(ServiceId id) : Exception($"Service with [id: {id}] is not found.")
{
    public static void ThrowIfNotContains(IServiceInstanceContanier contanier, ServiceId id)
    {
        if(contanier.HasInstance(id)) return;
        throw new ServiceInstanceNotFoundException(id);
    }
}