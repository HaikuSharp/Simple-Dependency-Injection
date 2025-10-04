using SDI.Abstraction;
using System;

namespace SDI.Accessing.Lazy.Scoping;

/// <summary>
/// Base class for service accessors that maintain one instance per service scope.
/// </summary>
public class ScopedServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    /// <inheritdoc/>
    protected override object Access(IServiceScopedProvider provider, ServiceId requestedId, ServiceId accessId)
    {
        var container = GetScope(provider, requestedId, accessId).Container;
        return container.HasInstance(accessId) ? container.GetInstance(accessId) : container.Create(accessId, CreateInstance(requestedId, provider));
    }

    /// <summary>
    /// When implemented in derived classes, gets the scope identifier for service instance management.
    /// </summary>
    /// <param name="provider">The service provider context.</param>
    /// <returns>The scope identifier that determines instance lifetime.</returns>
    /// <param name="requestedId">The service identifier to access.</param>
    /// <param name="accessId">The service identifier being providing.</param>
    protected virtual IServiceScopedProvider GetScope(IServiceScopedProvider provider, ServiceId requestedId, ServiceId accessId) => !provider.IsRoot ? provider : throw new InvalidOperationException($"Unable to obtain scoped service ({requestedId}) from origin provider.");
}