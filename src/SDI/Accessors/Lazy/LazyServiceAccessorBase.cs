using SDI.Abstraction;

namespace SDI.Accessors.Lazy;

public abstract class LazyServiceAccessorBase(ServiceId id, IServiceInstanceActivator activator) : ServiceAccessorBase(id)
{
    protected object Activate(IServiceProvider provider) => activator.Activate(provider);
}
