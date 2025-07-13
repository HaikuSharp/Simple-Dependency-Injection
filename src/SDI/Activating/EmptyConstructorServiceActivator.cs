using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

public sealed class EmptyConstructorServiceActivator(IServiceConstructor constructor) : ConstructorServiceActivatorBase(constructor)
{
    protected override object Activate(ServiceId requestedId, IServiceProvider provider, IServiceConstructor constructor) => constructor.Invoke([]);
}
