using SDI.Abstraction;
using SDI.Activating;

namespace SDI.Sculptor.Descripting.Abstraction;

public interface ILazyValueServiceDescriptor<TSelf> : IValueServiceDescriptor<TSelf> where TSelf : ILazyValueServiceDescriptor<TSelf>
{
    TSelf WithActivator(IServiceInstanceActivator activator);
}
