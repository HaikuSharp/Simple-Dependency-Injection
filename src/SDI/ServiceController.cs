using SDI.Abstraction;
using Sugar.Object.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using IServiceProvider = SDI.Abstraction.IServiceProvider;
namespace SDI;
public class ServiceController(IServiceProvider provider, IServiceDescriptAnalizer<Assembly> assemblyAnalizer, IServiceDescriptAnalizer<Type> typeAnalizer) : IServiceProvider {
 public bool IsImplemented(ServiceId id) {
  return provider.IsImplemented(id);
 }
 public IEnumerable GetServices(ServiceId id) {
  return provider.GetServices(id);
 }
 public object GetService(ServiceId id) {
  return provider.GetService(id);
 }
 public ServiceController RegisterService(Type type) {
  this.RegisterServices(typeAnalizer.Analize(type)).Forget();
  return this;
 }
 public ServiceController RegisterService(IServiceDescriptor descriptor) {
  provider.RegisterService(descriptor).Forget();
  return this;
 }
 public ServiceController RegisterServices(Assembly assembly) {
  this.RegisterServices(assemblyAnalizer.Analize(assembly)).Forget();
  return this;
 }
 public IServiceProvider RegisterServices(IEnumerable<IServiceDescriptor> descriptors) {
  provider.RegisterServices(descriptors).Forget();
  return this;
 }
 public IServiceProvider UnregisterService(ServiceId id) {
  provider.UnregisterService(id).Forget();
  return this;
 }
 public void Dispose() {
  provider.Dispose();
  GC.SuppressFinalize(this);
 }
 public static ServiceController Create() {
  return Create(new());
 }
 public static ServiceController Create(ServiceSetup setup) {
  return new(setup.CreateProvider(), setup.CreateAssemblyAnalizer(), setup.CreateTypeAnalizer());
 }

 IServiceProvider IServiceProvider.RegisterService(IServiceDescriptor descriptor) {
  return this.RegisterService(descriptor);
 }
}
