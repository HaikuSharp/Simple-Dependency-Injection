using SDI.Abstraction;

namespace SDI.Accessors;

public class SingletonServiceAccessor(ServiceId id) : ServiceAccessorBase(id)
{
    public override object Access(IServiceProvider provider) => ((IServiceInstanceContainer)provider.GetService(ServiceId.From<IServiceInstanceContainer>("self"))).GetInstance(Id);
}
