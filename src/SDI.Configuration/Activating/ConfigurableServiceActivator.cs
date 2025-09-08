using SDI.Abstraction;

namespace SDI.Configuration.Activating;

/// <summary>
/// An activator for creating and configuring services that implement <see cref="IConfigurableService"/>.
/// </summary>
/// <typeparam name="TService">The type of configurable service to activate.</typeparam>
public sealed class ConfigurableServiceActivator<TService> : IServiceInstanceActivator where TService : class, IConfigurableService, new()
{
    /// <summary>
    /// Gets the default instance of the configurable service activator.
    /// </summary>
    public static ConfigurableServiceActivator<TService> Default => field ??= new ConfigurableServiceActivator<TService>();

    /// <inheritdoc/>
    public object Activate(ServiceId requestedId, IServiceProvider provider)
    {
        TService instance = new();
        instance.Configure(provider);
        return instance;
    }
}