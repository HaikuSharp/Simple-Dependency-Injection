using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

/// <summary>
/// The exception that is thrown when attempting to register a service that already exists in the container.
/// </summary>
public sealed class ServiceAlreadyRegisteredException(ServiceId id) : Exception($"Service with [id: {id}] is already registered.")
{
    /// <summary>
    /// Throws a <see cref="ServiceAlreadyRegisteredException"/> if the specified service is already registered.
    /// </summary>
    /// <param name="registrar">The service registrar to check for existing registration.</param>
    /// <param name="id">The service identifier to verify.</param>
    /// <exception cref="ServiceAlreadyRegisteredException">
    /// Thrown when the service with specified <paramref name="id"/> is already registered in the <paramref name="registrar"/>.
    /// </exception>
    public static void ThrowIfRegistered(IServiceRegistrar registrar, ServiceId id)
    {
        if(!registrar.IsRegistered(id)) return;
        throw new ServiceAlreadyRegisteredException(id);
    }
}
