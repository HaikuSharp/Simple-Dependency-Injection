using SDI.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceProvider"/> to simplify service resolution.
/// </summary>
public static class ServiceProviderExtension
{
    /// <summary>
    /// Gets all services of type <typeparamref name="T"/> from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of services to resolve.</typeparam>
    /// <param name="provider">The service provider.</param>
    /// <returns>An enumerable of service instances of type <typeparamref name="T"/>.</returns>
    public static IEnumerable<T> GetServices<T>(this IServiceProvider provider) where T : class => provider.GetServices(ServiceId.From<T>()).Cast<T>();

    /// <summary>
    /// Gets all services of type <typeparamref name="T"/> with the specified key from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of services to resolve.</typeparam>
    /// <param name="provider">The service provider.</param>
    /// <param name="key">The key that identifies the services.</param>
    /// <returns>An enumerable of service instances of type <typeparamref name="T"/>.</returns>
    public static IEnumerable<T> GetServices<T>(this IServiceProvider provider, object key) where T : class => provider.GetServices(ServiceId.From<T>(key)).Cast<T>();

    /// <summary>
    /// Gets all services of the specified type from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="type">The type of services to resolve.</param>
    /// <returns>An enumerable of service instances.</returns>
    public static IEnumerable GetServices(this IServiceProvider provider, Type type) => provider.GetServices(ServiceId.FromType(type));

    /// <summary>
    /// Gets all services with the specified key from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="key">The key that identifies the services.</param>
    /// <returns>An enumerable of service instances.</returns>
    public static IEnumerable GetServices(this IServiceProvider provider, object key) => provider.GetServices(ServiceId.FromKey(key));

    /// <summary>
    /// Gets all services of the specified type with the specified key from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="type">The type of services to resolve.</param>
    /// <param name="key">The key that identifies the services.</param>
    /// <returns>An enumerable of service instances.</returns>
    public static IEnumerable GetServices(this IServiceProvider provider, Type type, object key) => provider.GetServices(ServiceId.FromType(type, key));

    /// <summary>
    /// Gets a service of type <typeparamref name="T"/> from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of service to resolve.</typeparam>
    /// <param name="provider">The service provider.</param>
    /// <returns>A service instance of type <typeparamref name="T"/> or null if not found.</returns>
    public static T GetService<T>(this IServiceProvider provider) where T : class => provider.GetService(ServiceId.From<T>()) as T;

    /// <summary>
    /// Gets a service of type <typeparamref name="T"/> with the specified key from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of service to resolve.</typeparam>
    /// <param name="provider">The service provider.</param>
    /// <param name="key">The key that identifies the service.</param>
    /// <returns>A service instance of type <typeparamref name="T"/> or null if not found.</returns>
    public static T GetService<T>(this IServiceProvider provider, object key) where T : class => provider.GetService(ServiceId.From<T>(key)) as T;

    /// <summary>
    /// Gets a service of the specified type from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="type">The type of service to resolve.</param>
    /// <returns>A service instance or null if not found.</returns>
    public static object GetService(this IServiceProvider provider, Type type) => provider.GetService(ServiceId.FromType(type));

    /// <summary>
    /// Gets a service with the specified key from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="key">The key that identifies the service.</param>
    /// <returns>A service instance or null if not found.</returns>
    public static object GetService(this IServiceProvider provider, object key) => provider.GetService(ServiceId.FromKey(key));

    /// <summary>
    /// Gets a service of the specified type with the specified key from the service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <param name="type">The type of service to resolve.</param>
    /// <param name="key">The key that identifies the service.</param>
    /// <returns>A service instance or null if not found.</returns>
    public static object GetService(this IServiceProvider provider, Type type, object key) => provider.GetService(ServiceId.FromType(type, key));
}