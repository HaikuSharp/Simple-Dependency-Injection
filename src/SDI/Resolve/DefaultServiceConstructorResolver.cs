using SDI.Abstraction;
using SDI.Invokers;
using System.Linq;

namespace SDI.Resolve;

public class DefaultServiceConstructorResolver : IServiceConstructorResolver
{
    public IServiceConstructorInvoker Resolve(IServiceDescriptor descriptor) => new ServiceConstructorInvoker(descriptor.ImplementationType.GetConstructors().OrderBy(c => c.GetParameters().Length).FirstOrDefault());
}