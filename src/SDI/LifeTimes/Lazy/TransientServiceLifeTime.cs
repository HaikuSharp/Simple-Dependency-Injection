using SDI.Abstraction;
using SDI.Accessors.Lazy;

namespace SDI.LifeTimes.Lazy;

public class TransientServiceLifeTime(IServiceActivatorResolver resolver) : LazyServiceLifeTimeBase(resolver)
{
    protected override IServiceAccessor CreateAccessor(IServiceDescriptor descriptor, IServiceInstanceActivator activator) => new TransientServiceAccessor(ServiceId.FromDescriptor(descriptor), activator);
}