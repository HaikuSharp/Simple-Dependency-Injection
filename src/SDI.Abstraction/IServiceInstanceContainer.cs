using System;

namespace SDI.Abstraction;

/// <summary>
/// Represents a container that manages service instances within a specific scope.
/// </summary>
public interface IServiceInstanceContainer : IDisposable
{
    /// <summary>
    /// Gets the scope identifier associated with this container.
    /// </summary>
    ScopeId Id { get; }

    /// <summary>
    /// Determines whether an instance of the specified service exists in this container.
    /// </summary>
    /// <param name="id">The service identifier to check.</param>
    /// <returns>
    /// <c>true</c> if the container holds an instance of the specified service;
    /// <c>false</c> otherwise.
    /// </returns>
    bool HasInstance(ServiceId id);

    /// <summary>
    /// Gets an existing instance of the specified service from this container.
    /// </summary>
    /// <param name="id">The service identifier to resolve.</param>
    /// <returns>
    /// The service instance if found; <c>null</c> if the instance doesn't exist.
    /// </returns>
    object GetInstance(ServiceId id);

    /// <summary>
    /// Creates and stores a new service instance in this container.
    /// </summary>
    /// <param name="id">The service identifier to associate with the instance.</param>
    /// <param name="instance">The service instance to store.</param>
    /// <returns>
    /// The created service instance (may be wrapped or modified by the container).
    /// </returns>
    object Create(ServiceId id, object instance);

    /// <summary>
    /// Disposes and removes a specific service instance from this container.
    /// </summary>
    /// <param name="id">The service identifier to dispose.</param>
    void Dispose(ServiceId id);
}