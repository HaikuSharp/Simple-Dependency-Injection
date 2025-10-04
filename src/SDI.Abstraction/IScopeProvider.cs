namespace SDI.Abstraction;

/// <summary>
/// Represents the base interface for a scope provider.
/// </summary>
public interface IScopeProvider
{
    /// <summary>
    /// Creates a new service provider scope with the specified identifier.
    /// </summary>
    /// <returns>
    /// A new <see cref="IServiceProvider"/> instance representing the created scope.
    /// </returns>
    IServiceProvider CreateScope();
}
