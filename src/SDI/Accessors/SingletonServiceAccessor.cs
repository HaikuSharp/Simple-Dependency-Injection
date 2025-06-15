using SDI.Abstraction;

namespace SDI.Accessors;

public class SingletonServiceAccessor(ServiceId id, IServiceInstanceProvider instanceProvider) : ServiceAccessorBase(id)
{
    public override object Access(IServiceProvider provider) => instanceProvider.GetInstance(Id);
}
