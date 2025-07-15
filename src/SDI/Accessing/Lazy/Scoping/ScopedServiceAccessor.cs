using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy.Scoping;

/// <summary>
/// Service accessor that maintains one service instance per provider scope.
/// </summary>
public class ScopedServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : ScopedServiceAccessorBase(id, activator)
{
    /// <summary>
    /// Gets the scope identifier from the current service provider.
    /// </summary>
    /// <param name="provider">The service provider context.</param>
    /// <returns>
    /// The <see cref="IServiceProvider.Id"/> of the current provider,
    /// which serves as the scope identifier for instance management.
    /// </returns>
    protected override ScopeId GetScopeId(IServiceProvider provider) => provider.Id;
}