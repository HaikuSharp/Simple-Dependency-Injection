using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public sealed class LazyCurrentScopedServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyScopedServiceAccessorBase(id, activator)
{
    protected override ScopeId GetScopeId(IServiceProvider provider) => provider.Id;
}
