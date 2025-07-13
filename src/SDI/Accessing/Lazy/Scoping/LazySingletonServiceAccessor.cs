using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy.Scoping;

public class LazySingletonServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : ScopedServiceAccessorBase(id, activator)
{
    protected override ScopeId GetScopeId(IServiceProvider provider) => ScopeId.Default;
}
