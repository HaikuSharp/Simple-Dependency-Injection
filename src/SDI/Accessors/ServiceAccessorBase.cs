using SDI.Abstraction;

namespace SDI.Accessors;

public abstract class ServiceAccessorBase(ServiceId id) : IServiceAccessor
{
    protected ServiceId Id => id;

    public bool CanAccess(ServiceId otherId) => otherId.Equals(id);

    public abstract object Access(IServiceProvider provider);
}
