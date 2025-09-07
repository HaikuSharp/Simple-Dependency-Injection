using SDI.Abstraction;

namespace SDI.Activating;

/// <summary>
/// An activator for creating services that don't require dependency injection.
/// </summary>
/// <typeparam name="TService">The type of service to activate.</typeparam>
public sealed class StandaloneServiceActivator<TService> : IServiceInstanceActivator where TService : class, new()
{
    /// <summary>
    /// Gets the default instance of the standalone service activator.
    /// </summary>
    public static StandaloneServiceActivator<TService> Default => field ??= new();

    /// <summary>
    /// Activates a new instance of the service using the default constructor.
    /// </summary>
    /// <param name="requestedId">The service identifier.</param>
    /// <param name="provider">The service provider (not used by this activator).</param>
    /// <returns>A new instance of the service.</returns>
    public object Activate(ServiceId requestedId, IServiceProvider provider) => new TService();
}