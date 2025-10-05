using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

/// <summary>
/// The exception that occurs when attempting to use a type on an id that has no type.
/// </summary>
public class InvalidServiceIdException(string message) : Exception(message)
{
    /// <summary>
    /// Throws a <see cref="InvalidServiceIdException"/> if the specified id has no type.
    /// </summary>
    /// <param name="id">The specified id.</param>
    /// <exception cref="InvalidServiceIdException">
    /// Thrown when <paramref name="id"/> has no type.
    /// </exception>
    public static void ThrowIfTypeIsNull(ServiceId id)
    {
        if(id.HasType) return;
        throw new InvalidServiceIdException($"The specified id ({id}) has no type.");
    }

    /// <summary>
    /// Throws a <see cref="InvalidServiceIdException"/> if the specified service is not implemented in the provider.
    /// </summary>
    /// <param name="provider">The service provider to check for implementation.</param>
    /// <param name="id">The service identifier to verify.</param>
    /// <exception cref="InvalidServiceIdException">
    /// Thrown when the service with specified <paramref name="id"/> is not implemented in the <paramref name="provider"/>.
    /// </exception>
    public static void ThrowIfNotImplemented(Abstraction.IServiceProvider provider, ServiceId id)
    {
        if(provider.IsImplemented(id)) return;
        throw new InvalidServiceIdException($"Service with [id: {id}] is not implemented.");
    }

    /// <summary>
    /// Throws a <see cref="InvalidServiceIdException"/> if the container already contains the specified service instance.
    /// </summary>
    /// <param name="container">The service instance container to check.</param>
    /// <param name="id">The service identifier to verify.</param>
    /// <exception cref="InvalidServiceIdException">
    /// Thrown when the container already has an instance for the specified <paramref name="id"/>.
    /// </exception>
    public static void ThrowIfContains(IServiceInstanceContainer container, ServiceId id)
    {
        if(!container.HasInstance(id)) return;
        throw new InvalidServiceIdException($"Service {id} is already added in the specified container.");
    }

    /// <summary>
    /// Throws a <see cref="InvalidServiceIdException"/> if the specified service is already registered.
    /// </summary>
    /// <param name="registrar">The service registrar to check for existing registration.</param>
    /// <param name="id">The service identifier to verify.</param>
    /// <exception cref="InvalidServiceIdException">
    /// Thrown when the service with specified <paramref name="id"/> is already registered in the <paramref name="registrar"/>.
    /// </exception>
    public static void ThrowIfRegistered(IServiceRegistrar registrar, ServiceId id)
    {
        if(!registrar.IsRegistered(id)) return;
        throw new InvalidServiceIdException($"Service {id} is already registered.");
    }

    /// <summary>
    /// Throws a <see cref="InvalidServiceIdException"/> if the specified service is not registered.
    /// </summary>
    /// <param name="registrar">The service registrar to check for registration.</param>
    /// <param name="id">The service identifier to verify.</param>
    /// <exception cref="InvalidServiceIdException">Thrown when the service is not registered.</exception>
    public static void ThrowIfNotRegistered(IServiceRegistrar registrar, ServiceId id)
    {
        if(registrar.IsRegistered(id)) return;
        throw new InvalidServiceIdException($"Service {id} is not registered.");
    }
}
