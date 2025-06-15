using SDI.Abstraction;

namespace SDI.Accessors.Lazy;

public class LazySingletonServiceAccessor(ServiceId id, IServiceInstanceActivator activator, IServiceInstanceContanier contanier) : LazyServiceAccessorBase(id, activator)
{
    public override object Access(IServiceProvider provider)
    {
        ServiceId id = Id;
        return contanier.TryGetInstance(id, out object instace) ? instace : contanier.Create(id, Activate(provider));
    }
}
