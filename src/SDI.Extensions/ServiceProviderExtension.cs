using SDI.Abstraction;
using Sugar.Object.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = SDI.Abstraction.IServiceProvider;
namespace SDI.Extensions;
public static class ServiceProviderExtension {
 public static IEnumerable<T> GetServices<T>(this IServiceProvider provider) where T : class {
  return provider.GetServices(ServiceId.From<T>()).Cast<T>();
 }
 public static IEnumerable<T> GetServices<T>(this IServiceProvider provider, object key) where T : class {
  return provider.GetServices(ServiceId.From<T>(key)).Cast<T>();
 }
 public static IEnumerable GetServices(this IServiceProvider provider, Type type) {
  return provider.GetServices(ServiceId.FromType(type));
 }
 public static IEnumerable GetServices(this IServiceProvider provider, Type type, object key) {
  return provider.GetServices(ServiceId.FromType(type, key));
 }
 public static T GetService<T>(this IServiceProvider provider) where T : class {
  return provider.GetService(ServiceId.From<T>()).As<T>();
 }
 public static T GetService<T>(this IServiceProvider provider, object key) where T : class {
  return provider.GetService(ServiceId.From<T>(key)).As<T>();
 }
 public static object GetService(this IServiceProvider provider, Type type) {
  return provider.GetService(ServiceId.FromType(type));
 }
 public static object GetService(this IServiceProvider provider, Type type, object key) {
  return provider.GetService(ServiceId.FromType(type, key));
 }
}
