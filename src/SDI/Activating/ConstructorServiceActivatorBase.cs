using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

public abstract class ConstructorServiceActivatorBase(IServiceConstructor constructor) : IServiceInstanceActivator
{
    public object Activate(ServiceId requestedId, IServiceProvider provider) => Activate(requestedId, provider, constructor);

    protected abstract object Activate(ServiceId requestedId, IServiceProvider provider, IServiceConstructor constructor);
}
