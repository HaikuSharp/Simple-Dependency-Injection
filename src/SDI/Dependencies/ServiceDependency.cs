using SDI.Abstraction;
using SDI.Helpers;
using Sugar.Object.Extensions;
using System;
namespace SDI.Dependencies;
public class ServiceDependency(Type serviceType, object key) : ServiceDependencyBase(serviceType, key) {
 public override object GetDependency(Abstraction.IServiceProvider provider, ServiceId id) {
  if(provider.IsImplemented(id)) {
   return provider.GetService(id);
  }
  var serviceType = EnumerableHelper.GetElementType(this.ServiceType);
  if(serviceType.IsNotNull) {
   return provider.GetServices(ServiceId.FromType(serviceType, this.Key)).ConvertToArray(serviceType);
  }
  return null;
 }
}