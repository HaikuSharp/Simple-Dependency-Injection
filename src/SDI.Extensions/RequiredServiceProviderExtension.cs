using SDI.Abstraction;
using SDI.Exceptions;
using Sugar.Object.Extensions;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;
namespace SDI.Extensions;
public static class RequiredServiceProviderExtension {
 public static T GetRequiredService<T>(this IServiceProvider provider) where T : class {
  return provider.GetRequiredService(ServiceId.From<T>()).As<T>();
 }
 public static T GetRequiredService<T>(this IServiceProvider provider, object key) where T : class {
  return provider.GetRequiredService(ServiceId.From<T>(key)).As<T>();
 }
 public static object GetRequiredService(this IServiceProvider provider, Type type) {
  return provider.GetRequiredService(ServiceId.FromType(type));
 }
 public static object GetRequiredService(this IServiceProvider provider, Type type, object key) {
  return provider.GetRequiredService(ServiceId.FromType(type, key));
 }
 public static object GetRequiredService(this IServiceProvider provider, ServiceId id) {
  ServiceNotImplementedException.ThrowIfNotImplemented(provider, id);
  var instance = provider.GetService(id);
  ServiceAccessException.ThrowIfServiceIntanceIsNull(id, instance);
  return instance;
 }
}
