namespace SDI.Abstraction;

public interface IServiceDependency
{
    object Resolve(IServiceProvider provider);
}
