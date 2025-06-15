using SDI.Abstraction;

namespace SDI.Accessors.Lazy;

public class TransientServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    public override object Access(IServiceProvider provider) => Activate(provider);
}
