using SDI.Abstraction;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

public abstract class ConstructorServiceActivatorBase(IServiceConstructor constructor) : IServiceInstanceActivator
{
    public Type ActivateType => constructor.DeclaringType;

    public object Activate(IServiceProvider provider) => Activate(provider, constructor);

    protected abstract object Activate(IServiceProvider provider, IServiceConstructor constructor);
}
