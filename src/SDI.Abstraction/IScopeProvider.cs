namespace SDI.Abstraction;

/// <summary>
/// Represents the base interface for a scope provider.
/// </summary>
public interface IScopeProvider
{
    /// <summary>
    /// Creates a new service provider scope with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the new scope.</param>
    /// <returns>
    /// A new <see cref="IServiceProvider"/> instance representing the created scope.
    /// </returns>
    IServiceProvider CreateScope(ScopeId id);
}
