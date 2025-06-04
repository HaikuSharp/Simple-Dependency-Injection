using SDI.Abstraction;
using SDI.Helpers;
using Sugar.Object.Extensions;
using System;
namespace SDI.Dependencies;
public abstract class ServiceDependencyBase(Type serviceType, object key) : IServiceDependency {
 public Type ServiceType {
  get {
   return serviceType;
  }
 }
 public object Key {
  get {
   return key;
  }
 }
 public object GetDependency(Abstraction.IServiceProvider provider) {
  return this.GetDependency(provider, ServiceId.FromType(serviceType, key));
 }
 public abstract object GetDependency(Abstraction.IServiceProvider provider, ServiceId id);
}
