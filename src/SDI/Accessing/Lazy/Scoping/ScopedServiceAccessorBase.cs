using SDI.Abstraction;
using SDI.Exceptions;
using SDI.Extensions;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy.Scoping;

/// <summary>
/// Base class for service accessors that maintain one instance per service scope.
/// </summary>
public abstract class ScopedServiceAccessorBase(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    /// <summary>
    /// Provides access to a scoped service instance, creating it if it doesn't exist in the current scope.
    /// </summary>
    /// <param name="provider">The service provider for dependency resolution.</param>
    /// <param name="requestedId">The service identifier being requested.</param>
    /// <returns>
    /// The service instance for the current scope - either existing or newly created.
    /// </returns>
    /// <exception cref="ScopeNotCreatedExeption">
    /// Thrown when the required scope cannot be accessed.
    /// </exception>
    public override object Access(IServiceProvider provider, ServiceId requestedId)
    {
        var scope = ScopeNotCreatedExeption.ThrowIfNull(provider.GetScope(GetScopeId(provider)));
        return scope.HasInstance(requestedId) ? scope.GetInstance(requestedId) : scope.Create(requestedId, CreateInstance(requestedId, provider));
    }

    /// <summary>
    /// When implemented in derived classes, gets the scope identifier for service instance management.
    /// </summary>
    /// <param name="provider">The service provider context.</param>
    /// <returns>The scope identifier that determines instance lifetime.</returns>
    protected abstract ScopeId GetScopeId(IServiceProvider provider);
}