using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Reflection.Activating;

public class ConstructorServiceActivator(IServiceConstructor constructor) : ConstructorServiceActivatorBase
{
    protected override IServiceConstructor GetConstructor(IServiceProvider provider) => constructor;
}