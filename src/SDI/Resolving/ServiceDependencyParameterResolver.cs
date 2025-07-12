using SDI.Abstraction;
using System.Reflection;

namespace SDI.Resolving;

public class ServiceDependencyParameterResolver : IServiceDependencyResolver<ParameterInfo>
{
    public IServiceDependency Resolve(ParameterInfo source) => new ServiceDependency(source.ParameterType, null);
}
