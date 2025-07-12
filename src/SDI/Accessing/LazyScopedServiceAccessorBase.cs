using SDI.Abstraction;
using SDI.Exceptions;
using SDI.Extensions;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public abstract class LazyScopedServiceAccessorBase(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    protected sealed override object Access(IServiceProvider provider, ServiceId id)
    {
        var scope = ScopeNotCreatedExeption.ThrowIfNull(provider.GetScope(GetScopeId(provider)));
        return scope.GetIsntance(id) ?? scope.Create(id, CreateInstance(provider));
    }

    protected abstract ScopeId GetScopeId(IServiceProvider provider);
}
