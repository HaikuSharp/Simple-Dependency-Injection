using SDI.Abstraction;
using SDI.Accessors;

namespace SDI.LifeTimes;

public class SingletonServiceLifeTime : IServiceLifeTime
{
    public IServiceAccessor CreateAccessor(IServiceDescriptor descriptor) => new SingletonServiceAccessor(ServiceId.FromDescriptor(descriptor));
}
