using SDI.Abstraction;
using System;
namespace SDI;
public class ServiceDescriptor(Type serviceType, Type implementationType, Type lifeTimeType, object key) : IServiceDescriptor {
 public Type ServiceType {
  get {
   return serviceType;
  }
 }
 public Type ImplementationType {
  get {
   return implementationType;
  }
 }
 public Type LifeTimeType {
  get {
   return lifeTimeType;
  }
 }
 public object Key {
  get {
   return key;
  }
 }
}
