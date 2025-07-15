namespace SDI.Abstraction;

/// <summary>
/// Represents a component that controls and provides access to services within the dependency injection system.
/// </summary>
public interface IServiceAccessor
{
    /// <summary>
    /// Determines whether the specified service can be accessed.
    /// </summary>
    /// <param name="requestedId">The service identifier to check for accessibility.</param>
    /// <returns>
    /// <c>true</c> if can be accessed under current conditions;
    /// <c>false</c> otherwise.
    /// </returns>
    bool CanAccess(ServiceId requestedId);

    /// <summary>
    /// Retrieves an instance of the requested service if accessible.
    /// </summary>
    /// <param name="provider">The service provider used for dependency resolution.</param>
    /// <param name="requestedId">The service identifier to access.</param>
    /// <returns>The resolved service instance.</returns>
    object Access(IServiceProvider provider, ServiceId requestedId);
}