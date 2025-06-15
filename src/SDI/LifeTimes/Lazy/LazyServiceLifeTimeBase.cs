using SDI.Abstraction;

namespace SDI.LifeTimes.Lazy;

public abstract class LazyServiceLifeTimeBase(IServiceActivatorResolver resolver) : IServiceLifeTime
{
    public IServiceAccessor CreateAccessor(IServiceInstanceContanier contanier, IServiceDescriptor descriptor) => CreateAccessor(contanier, descriptor, resolver.Resolve(descriptor));
    protected abstract IServiceAccessor CreateAccessor(IServiceInstanceContanier contanier, IServiceDescriptor descriptor, IServiceInstanceActivator activator);
}
