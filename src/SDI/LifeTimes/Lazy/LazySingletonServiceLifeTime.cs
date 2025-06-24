using SDI.Abstraction;
using SDI.Accessors.Lazy;

namespace SDI.LifeTimes.Lazy;

public class LazySingletonServiceLifeTime(IServiceActivatorResolver resolver) : LazyServiceLifeTimeBase(resolver)
{
    protected override IServiceAccessor CreateAccessor(IServiceDescriptor descriptor, IServiceInstanceActivator activator) => new LazySingletonServiceAccessor(ServiceId.FromDescriptor(descriptor), activator);
}
