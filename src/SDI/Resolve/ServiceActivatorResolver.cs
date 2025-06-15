using SDI.Abstraction;
using SDI.Activators;
using System.Linq;
using System.Reflection;
namespace SDI.Resolve;

public class ServiceActivatorResolver(IServiceConstructorResolver constructorResolver, IServiceDependencyResolver<ParameterInfo> dependencyResolver) : IServiceActivatorResolver
{
    public IServiceInstanceActivator Resolve(IServiceDescriptor descriptor)
    {
        IServiceConstructorInvoker constructor = constructorResolver.Resolve(descriptor);
        return new ServiceConstructorInstanceActivator(constructor, constructor.Parameters.Select(dependencyResolver.Resolve));
    }
}
