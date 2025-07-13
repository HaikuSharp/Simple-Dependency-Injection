namespace SDI.Abstraction;

public interface IServiceInstanceActivator
{
    object Activate(IServiceProvider provider);
}
