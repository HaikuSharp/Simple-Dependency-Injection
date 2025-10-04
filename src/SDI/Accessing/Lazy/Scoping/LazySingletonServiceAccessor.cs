using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy.Scoping;

/// <summary>
/// Service accessor that provides lazy-initialized singleton behavior,
/// creating and caching the instance on first access and reusing it for all subsequent requests.
/// </summary>
public sealed class LazySingletonServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : ScopedServiceAccessorBase(id, activator)
{
    /// <summary>
    /// Returns the default scope identifier to ensure singleton behavior across all requests.
    /// </summary>
    /// <param name="provider">The service provider context (unused for singleton scope).</param>
    /// <returns>
    /// <see cref="ScopeId.Default"/> to maintain a single instance
    /// regardless of the calling scope.
    /// </returns>
    protected override ScopeId GetScopeId(IServiceProvider provider) => ScopeId.Default;
}