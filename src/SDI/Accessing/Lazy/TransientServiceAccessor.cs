using SDI.Abstraction;
using SDI.Accessing.Lazy;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public class TransientServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    protected override object Access(IServiceProvider provider, ServiceId id) => CreateInstance(provider);
}
