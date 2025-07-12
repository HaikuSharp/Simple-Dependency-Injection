using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public sealed class InstanceServiceAccessor(ServiceId id, object instance) : ServiceAccessorBase(id)
{
    protected override object Access(IServiceProvider provider, ServiceId id) => instance;
}
