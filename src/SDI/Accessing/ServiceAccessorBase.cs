using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public abstract class ServiceAccessorBase(ServiceId accessId) : IServiceAccessor
{
    public bool CanAccess(ServiceId requestedId) => requestedId == accessId || (requestedId.IsGeneric && requestedId.IsClosedGeneric && accessId.IsGeneric && !accessId.IsClosedGeneric && requestedId.GenericDefinition == accessId);

    public abstract object Access(IServiceProvider provider, ServiceId requestedId);
}
