using SDI.Abstraction;

namespace SDI.Reflection.Abstraction;

/// <summary>
/// Represents a dependency that can be resolved to a service instance.
/// </summary>
public interface IServiceDependency
{
    /// <summary>
    /// Get the dependency service id.
    /// </summary>
    ServiceId Id { get; }

    /// <summary>
    /// Resolves the dependency to a service instance using the specified provider.
    /// </summary>
    /// <param name="provider">The service provider to use for dependency resolution.</param>
    /// <returns>The resolved service instance.</returns>
    object Resolve(IServiceProvider provider);
}