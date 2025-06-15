using SDI.Abstraction;
using SDI.Dependencies;
using System.Reflection;

namespace SDI.Resolve;

public class ServiceMethodParameterDependencyResolver(IServiceTypeResolver<ParameterInfo> typeResolver, IServiceKeyResolver<ParameterInfo> keyResolver) : IServiceDependencyResolver<ParameterInfo>
{
    public IServiceDependency Resolve(ParameterInfo source) => new ServiceDependency(typeResolver.Resolve(source) ?? source.ParameterType, keyResolver.Resolve(source));
}
