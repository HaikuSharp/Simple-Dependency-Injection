using SDI.Abstraction;
using SDI.Attribute;
using Sugar.Object.Extensions;
using System;
namespace SDI.Resolve.Inject;
public class ServiceAttributeDescriptTypeResolver : IServiceDescriptResolver<Type> {
 public IServiceDescriptor Resolve(Type source) {
  if(source.IsDefined(typeof(InjectDescriptAttribute), true) && source.IsClass && !source.IsAbstract && typeof(IServiceDescriptor).IsAssignableFrom(source)) {
   return Activator.CreateInstance(source).As<IServiceDescriptor>();
  }
  return null;
 }
}
