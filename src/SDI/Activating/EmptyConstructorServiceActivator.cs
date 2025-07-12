using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

public sealed class EmptyConstructorServiceActivator(IServiceConstructor constructor) : EmptyConstructorServiceActivatorBase(constructor)
{
    protected override object Activate(IServiceProvider provider, IServiceConstructor constructor) => constructor.Invoke([]);
}
