using SDI.Abstraction;

namespace SDI.Accessors;

public class ScriptableServiceAccessor(ServiceId id, ScriptableServiceAccessor.ServiceAccess access) : ServiceAccessorBase(id)
{
    public delegate object ServiceAccess(IServiceProvider provider);

    public override object Access(IServiceProvider provider) => access?.Invoke(provider);
}
