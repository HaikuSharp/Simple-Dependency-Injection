using SDI.Abstraction;
using SDI.Extensions;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy.Scoping;

public abstract class ScopedServiceAccessorBase(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    protected override object Access(IServiceProvider provider, ServiceId id)
    {
        var scope = provider.GetScope(GetScopeId(provider));
        return scope.HasInstance(id) ? scope.GetIsntance(id) : scope.Create(id, CreateInstance(provider));
    }

    protected abstract ScopeId GetScopeId(IServiceProvider provider);
}
