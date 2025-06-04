using SDI.Abstraction;
using SDI.Exceptions;
using Sugar.Object.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SDI;
public class ServiceInstanceContanier : IServiceInstanceContanier {
 private readonly List<ServiceInstance> m_Instances = [];
 public bool HasInstance(ServiceId id) {
  return this.m_Instances.Any(i => i.Id.Equals(id));
 }
 public object GetInstance(ServiceId id) {
  return this.InternalGetInstance(id)?.Instance;
 }
 public bool TryGetInstance(ServiceId id, out object instance) {
  instance = this.GetInstance(id);
  return instance.IsNotNull;
 }
 public object Create(ServiceId id, object instance) {
  ServiceInstanceAlreadyAddedException.ThrowIfContains(this, id);
  this.m_Instances.Add(new(id, instance));
  return instance;
 }
 public void Dispose() {
  this.DisposeAll();
  GC.SuppressFinalize(this);
 }
 public void Dispose(ServiceId id) {
  this.m_Instances.RemoveAll(i => DisposePredicate(i, id)).Forget();
 }
 public void DisposeAll() {
  var instances = this.m_Instances;
  foreach(var instance in instances) {
   instance.Dispose();
  }
  instances.Clear();
 }
 private ServiceInstance InternalGetInstance(ServiceId id) {
  return this.m_Instances.FirstOrDefault(i => i.Id.Equals(id));
 }
 private static bool DisposePredicate(ServiceInstance instance, ServiceId id) {
  if(instance.Id.Equals(id)) {
   instance.Dispose();
   return true;
  }
  return false;
 }
 private class ServiceInstance(ServiceId id, object instance) : IDisposable {
  internal ServiceId Id {
   get {
    return id;
   }
  }
  internal object Instance {
   get {
    return instance;
   }
  }
  public void Dispose() {
   if(instance.IsNotNull && instance.Is<IDisposable>(out var disposable)) {
    disposable.Dispose();
   }
  }
 }
}