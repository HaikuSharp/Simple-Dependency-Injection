namespace SDI.Abstraction;

public interface IServiceInstanceActivator
{
    object Activate(ServiceId requestedId, IServiceProvider provider);
}
