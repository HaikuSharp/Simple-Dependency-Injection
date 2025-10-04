using System;
using System.Collections;

namespace SDI.Abstraction;

/// <summary>
/// Represents the base interface for a service provider within a specific dependency injection scope.
/// Extends the standard <see cref="System.IServiceProvider"/> with scope-aware capabilities.
/// </summary>
public interface IServiceProvider : IScopeProvider, System.IServiceProvider, IDisposable
{
    /// <summary>
    /// Gets the service controller.
    /// </summary>
    IServiceController Controller { get; }

    /// <summary>
    /// Determines whether the specified service is registered in the current scope.
    /// </summary>
    /// <param name="id">The service identifier to check.</param>
    /// <returns>
    /// <c>true</c> if the service is registered in the current scope or parent scopes;
    /// <c>false</c> otherwise.
    /// </returns>
    bool IsImplemented(ServiceId id);

    /// <summary>
    /// Gets all registered instances of the specified service in the current scope hierarchy.
    /// </summary>
    /// <param name="id">The service identifier to resolve.</param>
    /// <returns>
    /// An enumerable of all service instances available in the current scope and parent scopes.
    /// </returns>
    IEnumerable GetServices(ServiceId id);

    /// <summary>
    /// Gets an instance of the specified service from the current scope hierarchy.
    /// </summary>
    /// <param name="id">The service identifier to resolve.</param>
    /// <returns>
    /// The resolved service instance from the current scope or parent scopes,
    /// or <c>null</c> if the service is not found.
    /// </returns>
    object GetService(ServiceId id);
}
