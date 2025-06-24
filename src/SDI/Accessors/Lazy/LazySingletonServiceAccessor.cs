using SDI.Abstraction;

namespace SDI.Accessors.Lazy;

public class LazySingletonServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    public override object Access(IServiceProvider provider)
    {
        var id = Id;
        IServiceInstanceContainer container = (IServiceInstanceContainer)provider.GetService(ServiceId.From<IServiceInstanceContainer>("self"));
        return container.TryGetInstance(id, out object instace) ? instace : container.Create(id, Activate(provider));
    }
}
