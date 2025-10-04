using SDI.Abstraction;

namespace SDI.Configuration;

/// <summary>
/// Defines a service has dependincies.
/// </summary>
public interface IConfigurableService
{
    /// <summary>
    /// Configures the service instance using the specified service provider.
    /// </summary>
    /// <param name="provider">The service provider used to resolve dependencies.</param>
    void Configure(IServiceScopedProvider provider);
}
