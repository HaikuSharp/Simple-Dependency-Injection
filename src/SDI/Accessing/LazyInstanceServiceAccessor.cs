using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public sealed class LazyInstanceServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyScopedServiceAccessorBase(id, activator)
{
    protected override ScopeId GetScopeId(IServiceProvider provider) => ScopeId.Default;
}
