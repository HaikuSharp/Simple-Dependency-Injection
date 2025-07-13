using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy;

public abstract class LazyServiceAccessorBase(ServiceId id, IServiceInstanceActivator activator) : ServiceAccessorBase(id)
{
    protected object CreateInstance(IServiceProvider provider) => activator.Activate(provider);
}
