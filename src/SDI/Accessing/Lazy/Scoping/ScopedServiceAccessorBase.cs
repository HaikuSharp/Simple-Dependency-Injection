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
    /// <inheritdoc/>
    protected override object Access(IServiceProvider provider, ServiceId requestedId, ServiceId accessId)
    {
        var id = GetScopeId(provider);
        var scope = ScopeNotCreatedExeption.ThrowIfNull(provider.GetScopeInstanceContainer(id), id);
        return scope.HasInstance(accessId) ? scope.GetInstance(accessId) : scope.Create(accessId, CreateInstance(requestedId, provider));
    }

    /// <summary>
    /// When implemented in derived classes, gets the scope identifier for service instance management.
    /// </summary>
    /// <param name="provider">The service provider context.</param>
    /// <returns>The scope identifier that determines instance lifetime.</returns>
    protected abstract ScopeId GetScopeId(IServiceProvider provider);
}