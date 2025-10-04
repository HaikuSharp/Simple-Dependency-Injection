using System.Collections;

namespace SDI.Abstraction;

/// <summary>
/// Provides functionality for accessing services by their identifiers.
/// </summary>
public interface IServiceAccessProvider
{
    /// <summary>
    /// Gets a service instance for the specified service identifier.
    /// </summary>
    /// <param name="id">The service identifier to resolve.</param>
    /// <param name="provider">The service provider used for dependency resolution.</param>
    /// <returns>The resolved service instance.</returns>
    object GetService(ServiceId id, IServiceScopedProvider provider);

    /// <summary>
    /// Gets all service instances for the specified service identifier.
    /// </summary>
    /// <param name="id">The service identifier to resolve.</param>
    /// <param name="provider">The service provider used for dependency resolution.</param>
    /// <returns>An enumerable of all resolved service instances for the specified identifier.</returns>
    IEnumerable GetServices(ServiceId id, IServiceScopedProvider provider);
}