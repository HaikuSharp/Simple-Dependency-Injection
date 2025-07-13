using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

public class ConstructorServiceActivator(IServiceConstructor constructor) : ConstructorServiceActivatorBase
{
    protected override IServiceConstructor GetConstructor(IServiceProvider provider) => constructor;
}