using SDI.Abstraction;

namespace SDI.Activating;

public sealed class ScriptableServiceActivator(ScriptableServiceActivator.Activator activator) : IServiceInstanceActivator
{
    public delegate object Activator(ServiceId requestedId, IServiceProvider provider);

    public object Activate(ServiceId requestedId, IServiceProvider provider) => activator(requestedId, provider);
}
