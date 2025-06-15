using SDI.Abstraction;
using SDI.Attribute;
using System;

namespace SDI.Resolve.Inject;

public class ServiceAttributeDescriptTypeResolver : IServiceDescriptResolver<Type>
{
    public IServiceDescriptor Resolve(Type source) => source.IsDefined(typeof(InjectDescriptAttribute), true) && source.IsClass && !source.IsAbstract && typeof(IServiceDescriptor).IsAssignableFrom(source) ? Activator.CreateInstance(source) as IServiceDescriptor : null;
}
