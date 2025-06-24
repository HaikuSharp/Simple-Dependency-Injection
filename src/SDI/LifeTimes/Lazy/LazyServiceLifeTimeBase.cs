using SDI.Abstraction;

namespace SDI.LifeTimes.Lazy;

public abstract class LazyServiceLifeTimeBase(IServiceActivatorResolver resolver) : IServiceLifeTime
{
    public IServiceAccessor CreateAccessor(IServiceDescriptor descriptor) => CreateAccessor(descriptor, resolver.Resolve(descriptor));

    protected abstract IServiceAccessor CreateAccessor(IServiceDescriptor descriptor, IServiceInstanceActivator activator);
}
