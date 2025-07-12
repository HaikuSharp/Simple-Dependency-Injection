using SDI.Abstraction;
using SDI.Extensions;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public abstract class ScopedServiceAccessorBase(ServiceId id) : ServiceAccessorBase(id)
{
    protected sealed override object Access(IServiceProvider provider, ServiceId id) => provider.GetIsntanceFromScope(id, GetScopeId(provider));

    protected abstract ScopeId GetScopeId(IServiceProvider provider);
}
