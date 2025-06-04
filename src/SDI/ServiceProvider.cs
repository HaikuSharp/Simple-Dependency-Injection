using SDI.Abstraction;
using SDI.Accessors;
using SDI.Exceptions;
using Sugar.Object.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = SDI.Abstraction.IServiceProvider;
namespace SDI;
public class ServiceProvider(IServiceInstanceContanier contanier, IServiceLifeTime lifeTime) : IServiceProvider {
 private readonly List<IServiceAccessor> m_Accessors = [];
 public IServiceProvider RegisterSelf() {
  this.InternalRegisterSelfAccessor(typeof(IServiceProvider), this);
  this.InternalRegisterSelfAccessor(typeof(IServiceInstanceContanier), contanier);
  this.InternalRegisterSelfAccessor(typeof(IServiceLifeTime), lifeTime);
  return this;
 }
 public bool IsImplemented(ServiceId id) {
  return this.m_Accessors.Any(a => a.CanAccess(id));
 }
 public IEnumerable GetServices(ServiceId id) {
  return this.m_Accessors.Where(a => a.CanAccess(id)).Select(a => a.Access(this));
 }
 public object GetService(ServiceId id) {
  return this.m_Accessors.FirstOrDefault(a => a.CanAccess(id))?.Access(this);
 }
 public IServiceProvider RegisterService(IServiceDescriptor descriptor) {
  this.InternalRegisterAccessor(this.CreateAccessorAndVerifyRegistration(descriptor));
  return this;
 }
 public IServiceProvider RegisterServices(IEnumerable<IServiceDescriptor> descriptors) {
  this.m_Accessors.AddRange(descriptors.Select(this.CreateAccessorAndVerifyRegistration));
  return this;
 }
 public IServiceProvider UnregisterService(ServiceId id) {
  ServiceNotImplementedException.ThrowIfNotImplemented(this, id);
  this.m_Accessors.RemoveAll(a => a.CanAccess(id)).Forget();
  return this;
 }
 public void Dispose() {
  contanier.DisposeAll();
  GC.SuppressFinalize(this);
 }
 private void InternalRegisterSelfAccessor(Type type, object instance) {
  this.InternalRegisterAccessor(CreateSelfAccessor(type, instance));
 }
 private void InternalRegisterAccessor(IServiceAccessor accessor) {
  this.m_Accessors.Add(accessor);
 }
 private IServiceAccessor CreateAccessorAndVerifyRegistration(IServiceDescriptor descriptor) {
  ServiceAlreadyImplementedException.ThrowIfImplemented(this, ServiceId.FromDescriptor(descriptor));
  return lifeTime.CreateAccessor(contanier, descriptor);
 }
 private static IServiceAccessor CreateSelfAccessor(Type type, object instance) {
  return CreateSelfAccessor(ServiceId.FromType(type, "self"), instance);
 }
 private static IServiceAccessor CreateSelfAccessor(ServiceId id, object instance) {
  return new InstanceServiceAccessor(id, instance);
 }
}
