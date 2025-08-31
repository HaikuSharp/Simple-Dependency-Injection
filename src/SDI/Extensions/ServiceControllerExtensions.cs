using SDI.Abstraction;
using SDI.Descripting;
using SDI.Descripting.Lazy;
using System;

namespace SDI.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceController"/> to simplify service registration.
/// </summary>
public static class ServiceControllerExtensions
{
    /// <summary>
    /// Registers a scoped service with the specified activator.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterScopedService<TService>(this IServiceController controller, object key, IServiceInstanceActivator activator) where TService : class => controller.RegisterScopedService(typeof(TService), key, activator);

    /// <summary>
    /// Registers a transient service with the specified activator.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterTransientService<TService>(this IServiceController controller, object key, IServiceInstanceActivator activator) where TService : class => controller.RegisterTransientService(typeof(TService), key, activator);

    /// <summary>
    /// Registers a lazy singleton service with the specified activator.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create the singleton instance.</param>
    public static void RegisterLazySingletonService<TService>(this IServiceController controller, object key, IServiceInstanceActivator activator) where TService : class => controller.RegisterLazySingletonService(typeof(TService), key, activator);

    /// <summary>
    /// Registers a weakly-referenced singleton service instance.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterWeakSingletonService<TService>(this IServiceController controller, object key, TService instance) where TService : class => controller.RegisterWeakSingletonService(typeof(TService), key, instance);

    /// <summary>
    /// Registers a strongly-referenced singleton service instance.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterSingletonService<TService>(this IServiceController controller, object key, TService instance) where TService : class => controller.RegisterSingletonService(typeof(TService), key, instance);

    /// <summary>
    /// Registers a scoped service with the specified activator.
    /// </summary>
    /// <param name="controller">The service controller.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterScopedService(this IServiceController controller, Type serviceType, object key, IServiceInstanceActivator activator) => controller.RegisterService(new ScopedServiceDescriptor(serviceType, key, activator));

    /// <summary>
    /// Registers a transient service with the specified activator.
    /// </summary>
    /// <param name="controller">The service controller.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterTransientService(this IServiceController controller, Type serviceType, object key, IServiceInstanceActivator activator) => controller.RegisterService(new TransientServiceDescriptor(serviceType, key, activator));

    /// <summary>
    /// Registers a lazy singleton service with the specified activator.
    /// </summary>
    /// <param name="controller">The service controller.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create the singleton instance.</param>
    public static void RegisterLazySingletonService(this IServiceController controller, Type serviceType, object key, IServiceInstanceActivator activator) => controller.RegisterService(new LazySingletonServiceDescriptor(serviceType, key, activator));

    /// <summary>
    /// Registers a weakly-referenced singleton service instance.
    /// </summary>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterWeakSingletonService(this IServiceController controller, object key, object instance) => controller.RegisterWeakSingletonService(instance.GetType(), key, instance);

    /// <summary>
    /// Registers a strongly-referenced singleton service instance.
    /// </summary>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterSingletonService(this IServiceController controller, object key, object instance) => controller.RegisterSingletonService(instance.GetType(), key, instance);

    /// <summary>
    /// Registers a weakly-referenced singleton service instance.
    /// </summary>
    /// <param name="controller">The service controller.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterWeakSingletonService(this IServiceController controller, Type serviceType, object key, object instance) => controller.RegisterService(new WeakSingletonServiceDescriptor(serviceType, key, instance));

    /// <summary>
    /// Registers a strongly-referenced singleton service instance.
    /// </summary>
    /// <param name="controller">The service controller.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterSingletonService(this IServiceController controller, Type serviceType, object key, object instance) => controller.RegisterService(new SingletonServiceDescriptor(serviceType, key, instance));
}