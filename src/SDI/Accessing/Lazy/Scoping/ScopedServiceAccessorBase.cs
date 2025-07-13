using SDI.Abstraction;
using SDI.Exceptions;
using SDI.Extensions;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy.Scoping;

public abstract class ScopedServiceAccessorBase(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    public override object Access(IServiceProvider provider, ServiceId requestedId)
    {
        var scope = ScopeNotCreatedExeption.ThrowIfNull(provider.GetScope(GetScopeId(provider)));
        return scope.HasInstance(requestedId) ? scope.GetIsntance(requestedId) : scope.Create(requestedId, CreateInstance(requestedId, provider));
    }

    protected abstract ScopeId GetScopeId(IServiceProvider provider);
}
