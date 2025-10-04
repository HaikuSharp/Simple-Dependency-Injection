namespace SDI.Abstraction;

/// <summary>
/// Represents the base interface for a service scoped provider.
/// </summary>
public interface IServiceScopedProvider : IServiceProvider
{
    /// <summary>
    /// Indicates whether the current provider is root.
    /// </summary>
    bool IsRoot { get; }

    /// <summary>
    /// Gets the root service scoped provider.
    /// </summary>
    IServiceScopedProvider Root { get; }

    /// <summary>
    /// Gets the service scoped instance container.
    /// </summary>
    IServiceInstanceContainer Container { get; }
}