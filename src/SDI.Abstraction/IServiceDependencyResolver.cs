namespace SDI.Abstraction;

public interface IServiceDependencyResolver<in TDependencySource>
{
    IServiceDependency Resolve(TDependencySource source);
}
