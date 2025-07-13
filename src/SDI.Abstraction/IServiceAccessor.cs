namespace SDI.Abstraction;

public interface IServiceAccessor
{
    bool CanAccess(ServiceId requestedId);

    object Access(IServiceProvider provider, ServiceId requestedId);
}
