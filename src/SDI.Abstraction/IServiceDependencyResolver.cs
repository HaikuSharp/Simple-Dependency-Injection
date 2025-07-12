namespace SDI.Abstraction;

public interface IServiceDependencyResolver
{
    IServiceDependency Resolve(ServiceDependencyInfo dependencyInfo);
}