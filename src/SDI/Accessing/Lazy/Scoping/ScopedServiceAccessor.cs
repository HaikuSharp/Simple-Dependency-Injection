using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy.Scoping;

public class ScopedServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : ScopedServiceAccessorBase(id, activator)
{
    protected override ScopeId GetScopeId(IServiceProvider provider) => provider.Id;
}
