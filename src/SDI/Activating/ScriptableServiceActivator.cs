using SDI.Abstraction;

namespace SDI.Activating;

public class ScriptableServiceActivator(ScriptableServiceActivator.Activator activator) : IServiceInstanceActivator
{
    public delegate object Activator(IServiceProvider provider);

    public object Activate(IServiceProvider provider) => activator(provider);
}
