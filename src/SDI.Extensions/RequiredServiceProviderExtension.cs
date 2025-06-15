using SDI.Abstraction;
using SDI.Exceptions;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Extensions;

public static class RequiredServiceProviderExtension
{
    public static T GetRequiredService<T>(this IServiceProvider provider) where T : class => provider.GetRequiredService(ServiceId.From<T>()) as T;

    public static T GetRequiredService<T>(this IServiceProvider provider, object key) where T : class => provider.GetRequiredService(ServiceId.From<T>(key)) as T;

    public static object GetRequiredService(this IServiceProvider provider, Type type) => provider.GetRequiredService(ServiceId.FromType(type));

    public static object GetRequiredService(this IServiceProvider provider, Type type, object key) => provider.GetRequiredService(ServiceId.FromType(type, key));

    public static object GetRequiredService(this IServiceProvider provider, ServiceId id)
    {
        ServiceNotImplementedException.ThrowIfNotImplemented(provider, id);
        object instance = provider.GetService(id);
        ServiceAccessException.ThrowIfServiceIntanceIsNull(id, instance);
        return instance;
    }
}
