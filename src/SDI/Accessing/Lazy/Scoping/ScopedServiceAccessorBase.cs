using SDI.Abstraction;
using SDI.Accessing.Lazy;
using SDI.Extensions;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public abstract class ScopedServiceAccessorBase(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    protected override object Access(IServiceProvider provider, ServiceId id)
    {
        var scope = provider.GetScope(GetScopeId(provider));
        return scope.HasInstance(id) ? scope.GetIsntance(id) : scope.Create(id, CreateInstance(provider));
    }

    protected abstract ScopeId GetScopeId(IServiceProvider provider);
}
