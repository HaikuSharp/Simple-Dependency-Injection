using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public abstract class ServiceAccessorBase(ServiceId id) : IServiceAccessor
{
    public bool CanAccess(ServiceId otherId) => otherId == id;

    public object Access(IServiceProvider provider) => Access(provider, id);

    protected abstract object Access(IServiceProvider provider, ServiceId id);
}
