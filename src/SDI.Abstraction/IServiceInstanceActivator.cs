using System;

namespace SDI.Abstraction;

public interface IServiceInstanceActivator
{
    Type ActivateType { get; }

    object Activate(IServiceProvider provider);
}
