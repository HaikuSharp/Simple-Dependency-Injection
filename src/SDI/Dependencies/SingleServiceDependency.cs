using SDI.Abstraction;
using System;
namespace SDI.Dependencies;
public class SingleServiceDependency(Type serviceType, object key) : ServiceDependencyBase(serviceType, key) {
 public override object GetDependency(Abstraction.IServiceProvider provider, ServiceId id) {
  return provider.GetService(id);
 }
}
