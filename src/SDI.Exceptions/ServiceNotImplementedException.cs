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

public class ScopeNotCreatedExeption(ScopeId id) : Exception($"Scope with [id: {id}] not created.")
{
    public static IServiceInstanceContainer ThrowIfNull(IServiceInstanceContainer container) => container is not null ? container : throw new ScopeNotCreatedExeption(container.Id);
}