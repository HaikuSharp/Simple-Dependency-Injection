using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public sealed class CurrentScopedServiceAccessor(ServiceId serviceId) : ScopedServiceAccessorBase(serviceId)
{
    protected override ScopeId GetScopeId(IServiceProvider provider) => provider.Id;
}
