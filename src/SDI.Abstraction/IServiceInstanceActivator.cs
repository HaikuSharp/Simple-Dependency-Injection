namespace SDI.Abstraction;

/// <summary>
/// Represents a component capable of activating service instances on demand.
/// </summary>
public interface IServiceInstanceActivator
{
    /// <summary>
    /// Creates and activates an instance of the requested service.
    /// </summary>
    /// <param name="requestedId">The service identifier to activate.</param>
    /// <param name="provider">The service provider that can be used for dependency resolution.</param>
    /// <returns>The newly activated service instance.</returns>
    object Activate(ServiceId requestedId, IServiceScopedProvider provider);
}