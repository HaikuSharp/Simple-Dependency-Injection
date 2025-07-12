using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public sealed class FixedScopedServiceAccessor(ServiceId serviceId, ScopeId scopeId) : ScopedServiceAccessorBase(serviceId)
{
    protected override ScopeId GetScopeId(IServiceProvider provider) => scopeId;
}
