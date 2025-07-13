using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy;

public sealed class TransientServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    public override object Access(IServiceProvider provider, ServiceId requestedId) => CreateInstance(requestedId, provider);
}
