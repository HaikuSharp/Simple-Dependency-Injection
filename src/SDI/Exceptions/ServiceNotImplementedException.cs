using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

/// <summary>
/// The exception that is thrown when attempting to access a service that has not been registered in the container.
/// </summary>
public class ServiceNotImplementedException(ServiceId id) : Exception($"Service with [id: {id}] is not implemented.")
{
    /// <summary>
    /// Throws a <see cref="ServiceNotImplementedException"/> if the specified service is not implemented in the provider.
    /// </summary>
    /// <param name="provider">The service provider to check for implementation.</param>
    /// <param name="id">The service identifier to verify.</param>
    /// <exception cref="ServiceNotImplementedException">
    /// Thrown when the service with specified <paramref name="id"/> is not implemented in the <paramref name="provider"/>.
    /// </exception>
    public static void ThrowIfNotImplemented(Abstraction.IServiceProvider provider, ServiceId id)
    {
        if(provider.IsImplemented(id)) return;
        throw new ServiceNotImplementedException(id);
    }
}