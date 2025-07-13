using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy;

public abstract class LazyServiceAccessorBase(ServiceId id, IServiceInstanceActivator activator) : ServiceAccessorBase(id)
{
    protected object CreateInstance(ServiceId requestedId, IServiceProvider provider) => activator.Activate(requestedId, provider);
}
