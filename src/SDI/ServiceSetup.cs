using SDI.Abstraction;
using SDI.Analizers;
using SDI.LifeTimes;
using SDI.LifeTimes.Lazy;
using SDI.Resolve;
using SDI.Resolve.Inject;
using System;
using System.Collections.Generic;
using System.Reflection;
using IServiceProvider = SDI.Abstraction.IServiceProvider;
namespace SDI;
public class ServiceSetup {
 public virtual IServiceProvider CreateProvider() {
  return new ServiceProvider(this.CreateInstanceContanier(), this.CreateRootLifeTime()).RegisterSelf();
 }
 public IServiceDescriptAnalizer<Type> CreateTypeAnalizer() {
  return new InjectTypeServiceDescriptAnalizer(this.CreateAttributeTypeResolver<Type>(), this.CreateAttributeKeyResolver<Type>(), this.CreateAttributeLifeTimeResolver<Type>());
 }
 public virtual IServiceDescriptAnalizer<Assembly> CreateAssemblyAnalizer() {
  return new AssemblyServiceDescriptAnalizer(this.CreateTypeAnalizer());
 }
 protected virtual IServiceInstanceContanier CreateInstanceContanier() {
  return new ServiceInstanceContanier();
 }
 protected virtual IServiceLifeTime CreateRootLifeTime() {
  return new ServiceLifeTimeManager(this.CreateServiceLifeTimes());
 }
 protected virtual IEnumerable<IServiceLifeTime> CreateServiceLifeTimes() {
  return [new SingletonServiceLifeTime(), new LazySingletonServiceLifeTime(this.CreateActivatorResolver()), new TransientServiceLifeTime(this.CreateActivatorResolver())];
 }
 protected virtual IServiceActivatorResolver CreateActivatorResolver() {
  return new ServiceActivatorResolver(this.CreateConstructorResolver(), this.CreateMethodParameterDependencyResolver());
 }
 protected virtual IServiceConstructorResolver CreateConstructorResolver() {
  return new DefaultServiceConstructorResolver();
 }
 protected virtual IServiceDependencyResolver<ParameterInfo> CreateMethodParameterDependencyResolver() {
  return new ServiceMethodParameterDependencyResolver(this.CreateAttributeTypeResolver<ParameterInfo>(), this.CreateAttributeKeyResolver<ParameterInfo>());
 }
 protected virtual IServiceTypeResolver<T> CreateAttributeTypeResolver<T>() where T : ICustomAttributeProvider {
  return new ServiceAttributeTypeResolver<T>();
 }
 protected virtual IServiceKeyResolver<T> CreateAttributeKeyResolver<T>() where T : ICustomAttributeProvider {
  return new ServiceAttributeKeyResolver<T>();
 }
 protected virtual IServiceLifeTimeTypeResolver<T> CreateAttributeLifeTimeResolver<T>() where T : ICustomAttributeProvider {
  return new ServiceAttributeLifeTimeTypeResolver<T>();
 }
 protected virtual IServiceDescriptResolver<Type> CreateAttributeTypeDescriptResolver<T>() {
  return new ServiceAttributeDescriptTypeResolver();
 }
}
