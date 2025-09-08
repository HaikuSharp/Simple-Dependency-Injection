using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

/// <summary>
/// The exception that is thrown when attempting to add a duplicate service instance to a container.
/// </summary>
public sealed class ServiceInstanceAlreadyAddedException(ServiceId id) : Exception($"Service with [id: {id}] is already added.")
{
    /// <summary>
    /// Throws a <see cref="ServiceInstanceAlreadyAddedException"/> if the container already contains the specified service instance.
    /// </summary>
    /// <param name="container">The service instance container to check.</param>
    /// <param name="id">The service identifier to verify.</param>
    /// <exception cref="ServiceInstanceAlreadyAddedException">
    /// Thrown when the container already has an instance for the specified <paramref name="id"/>.
    /// </exception>
    public static void ThrowIfContains(IServiceInstanceContainer container, ServiceId id)
    {
        if(!container.HasInstance(id)) return;
        throw new ServiceInstanceAlreadyAddedException(id);
    }
}