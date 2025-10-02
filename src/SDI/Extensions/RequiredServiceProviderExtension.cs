using SDI.Abstraction;
using SDI.Exceptions;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceProvider"/> that throw exceptions when services are not available.
/// </summary>
public static class RequiredServiceProviderExtension
{
    /// <summary>
    /// Gets a required service of type <typeparamref name="T"/> from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of service object to get.</typeparam>
    /// <param name="provider">The service provider.</param>
    /// <returns>The requested service instance.</returns>
    /// <exception cref="ServiceNotImplementedException">
    /// Thrown when the service is not registered in the provider.
    /// </exception>
    /// <exception cref="ServiceAccessException">
    /// Thrown when the service instance could not be created or accessed.
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// Thrown when the service type is not <typeparamref name="T"/>
    /// </exception>
    public static T GetRequiredService<T>(this IServiceProvider provider) where T : class => ServiceCast<T>(provider.GetRequiredService(ServiceId.From<T>()));

    /// <summary>
    /// Gets a required service of type <typeparamref name="T"/> with the specified key from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of service object to get.</typeparam>
    /// <param name="provider">The service provider.</param>
    /// <param name="key">The key that identifies the service instance.</param>
    /// <returns>The requested service instance.</returns>
    /// <exception cref="ServiceNotImplementedException">
    /// Thrown when the service is not registered in the provider.
    /// </exception>
    /// <exception cref="ServiceAccessException">
    /// Thrown when the service instance could not be created or accessed.
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// Thrown when the service type is not <typeparamref name="T"/>
    /// </exception>
    public static T GetRequiredService<T>(this IServiceProvider provider, object key) where T : class => ServiceCast<T>(provider.GetRequiredService(ServiceId.From<T>(key)));

    /// <summary>
    /// Gets a required service of the specified type from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="type">The type of service object to get.</param>
    /// <returns>The requested service instance.</returns>
    /// <exception cref="ServiceNotImplementedException">
    /// Thrown when the service is not registered in the provider.
    /// </exception>
    /// <exception cref="ServiceAccessException">
    /// Thrown when the service instance could not be created or accessed.
    /// </exception>
    public static object GetRequiredService(this IServiceProvider provider, Type type) => provider.GetRequiredService(ServiceId.FromType(type));

    /// <summary>
    /// Gets a required service with the specified key from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="key">The key that identifies the service instance.</param>
    /// <returns>The requested service instance.</returns>
    /// <exception cref="ServiceNotImplementedException">
    /// Thrown when the service is not registered in the provider.
    /// </exception>
    /// <exception cref="ServiceAccessException">
    /// Thrown when the service instance could not be created or accessed.
    /// </exception>
    public static object GetRequiredService(this IServiceProvider provider, object key) => provider.GetRequiredService(ServiceId.FromKey(key));

    /// <summary>
    /// Gets a required service of the specified type with the specified key from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="type">The type of service object to get.</param>
    /// <param name="key">The key that identifies the service instance.</param>
    /// <returns>The requested service instance.</returns>
    /// <exception cref="ServiceNotImplementedException">
    /// Thrown when the service is not registered in the provider.
    /// </exception>
    /// <exception cref="ServiceAccessException">
    /// Thrown when the service instance could not be created or accessed.
    /// </exception>
    public static object GetRequiredService(this IServiceProvider provider, Type type, object key) => provider.GetRequiredService(ServiceId.FromType(type, key));

    /// <summary>
    /// Gets a required service identified by <see cref="ServiceId"/> from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="id">The service identifier.</param>
    /// <returns>The requested service instance.</returns>
    /// <exception cref="ServiceNotImplementedException">
    /// Thrown when the service is not registered in the provider.
    /// </exception>
    /// <exception cref="ServiceAccessException">
    /// Thrown when the service instance could not be created or accessed.
    /// </exception>
    public static object GetRequiredService(this IServiceProvider provider, ServiceId id)
    {
        ServiceNotImplementedException.ThrowIfNotImplemented(provider, id);
        object instance = provider.GetService(id);
        ServiceAccessException.ThrowIfServiceInstanceIsNull(id, instance);
        return instance;
    }

    private static T ServiceCast<T>(object service) where T : class => service is T tservice ? tservice : throw new InvalidCastException($"The service type ({service.GetType().FullName}) does not match the requested one ({typeof(T).FullName}).");
}