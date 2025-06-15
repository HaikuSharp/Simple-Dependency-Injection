using SDI.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Extensions;

public static class ServiceProviderExtension
{
    public static IEnumerable<T> GetServices<T>(this IServiceProvider provider) where T : class => provider.GetServices(ServiceId.From<T>()).Cast<T>();

    public static IEnumerable<T> GetServices<T>(this IServiceProvider provider, object key) where T : class => provider.GetServices(ServiceId.From<T>(key)).Cast<T>();

    public static IEnumerable GetServices(this IServiceProvider provider, Type type) => provider.GetServices(ServiceId.FromType(type));

    public static IEnumerable GetServices(this IServiceProvider provider, Type type, object key) => provider.GetServices(ServiceId.FromType(type, key));

    public static T GetService<T>(this IServiceProvider provider) where T : class => provider.GetService(ServiceId.From<T>()) as T;

    public static T GetService<T>(this IServiceProvider provider, object key) where T : class => provider.GetService(ServiceId.From<T>(key)) as T;

    public static object GetService(this IServiceProvider provider, Type type) => provider.GetService(ServiceId.FromType(type));

    public static object GetService(this IServiceProvider provider, Type type, object key) => provider.GetService(ServiceId.FromType(type, key));
}
