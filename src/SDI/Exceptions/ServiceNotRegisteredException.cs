using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

/// <summary>
/// Exception thrown when attempting to resolve a service that has not been registered.
/// </summary>
/// <param name="id">The service identifier that was not found.</param>
public sealed class ServiceNotRegisteredException(ServiceId id) : Exception($"Service with [id: {id}] is not registered.")
{
    /// <summary>
    /// Throws a <see cref="ServiceNotRegisteredException"/> if the specified service is not registered.
    /// </summary>
    /// <param name="registrar">The service registrar to check for registration.</param>
    /// <param name="id">The service identifier to verify.</param>
    /// <exception cref="ServiceNotRegisteredException">Thrown when the service is not registered.</exception>
    public static void ThrowIfNotRegistered(IServiceRegistrar registrar, ServiceId id)
    {
        if(registrar.IsRegistered(id)) return;
        throw new ServiceNotRegisteredException(id);
    }
}