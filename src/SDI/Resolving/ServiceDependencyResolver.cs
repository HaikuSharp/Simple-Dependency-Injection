using SDI.Abstraction;

namespace SDI.Resolving;

public class ServiceDependencyResolver : IServiceDependencyResolver
{
    public static ServiceDependencyResolver Default => field ??= new();

    public IServiceDependency Resolve(ServiceDependencyInfo dependencyInfo) => new ServiceDependency(dependencyInfo.SourceType, null);
}
