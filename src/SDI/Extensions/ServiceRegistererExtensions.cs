using SDI.Abstraction;
using SDI.Activating;
using SDI.Descripting;
using SDI.Descripting.Lazy;
using System;

namespace SDI.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceRegistrar"/> to simplify service registration.
/// </summary>
public static class ServiceRegistererExtensions
{
    /// <summary>
    /// Registers a scoped standalone service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterScopedStandaloneService<TService>(this IServiceRegistrar registrar, object key) where TService : class, new() => registrar.RegisterScopedStandaloneService<TService, TService>(key);

    /// <summary>
    /// Registers a transient standalone service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterTransientStandaloneService<TService>(this IServiceRegistrar registrar, object key) where TService : class, new() => registrar.RegisterTransientStandaloneService<TService, TService>(key);

    /// <summary>
    /// Registers a lazy singleton standalone service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterLazySingletonStandaloneService<TService>(this IServiceRegistrar registrar, object key) where TService : class, new() => registrar.RegisterLazySingletonStandaloneService<TService, TService>(key);

    /// <summary>
    /// Registers a scoped standalone service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that must have a parameterless constructor.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterScopedStandaloneService<TService, TImplementation>(this IServiceRegistrar registrar, object key) where TService : class where TImplementation : class, TService, new() => registrar.RegisterScopedService<TService>(key, StandaloneServiceActivator<TImplementation>.Default);

    /// <summary>
    /// Registers a transient standalone service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that must have a parameterless constructor.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterTransientStandaloneService<TService, TImplementation>(this IServiceRegistrar registrar, object key) where TService : class where TImplementation : class, TService, new() => registrar.RegisterTransientService<TService>(key, StandaloneServiceActivator<TImplementation>.Default);

    /// <summary>
    /// Registers a lazy singleton standalone service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that must have a parameterless constructor.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterLazySingletonStandaloneService<TService, TImplementation>(this IServiceRegistrar registrar, object key) where TService : class where TImplementation : class, TService, new() => registrar.RegisterLazySingletonService<TService>(key, StandaloneServiceActivator<TImplementation>.Default);

    /// <summary>
    /// Registers a scoped service with the specified activator.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterScopedService<TService>(this IServiceRegistrar registrar, object key, ScriptableServiceActivator.Activator activator) where TService : class => registrar.RegisterScopedService(typeof(TService), key, activator);

    /// <summary>
    /// Registers a transient service with the specified activator.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterTransientService<TService>(this IServiceRegistrar registrar, object key, ScriptableServiceActivator.Activator activator) where TService : class => registrar.RegisterTransientService(typeof(TService), key, activator);

    /// <summary>
    /// Registers a lazy singleton service with the specified activator.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create the singleton instance.</param>
    public static void RegisterLazySingletonService<TService>(this IServiceRegistrar registrar, object key, ScriptableServiceActivator.Activator activator) where TService : class => registrar.RegisterLazySingletonService(typeof(TService), key, activator);

    /// <summary>
    /// Registers a scoped service with the specified activator.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterScopedService(this IServiceRegistrar registrar, Type serviceType, object key, ScriptableServiceActivator.Activator activator) => registrar.RegisterService(new ScopedServiceDescriptor(serviceType, key, new ScriptableServiceActivator(activator)));

    /// <summary>
    /// Registers a transient service with the specified activator.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterTransientService(this IServiceRegistrar registrar, Type serviceType, object key, ScriptableServiceActivator.Activator activator) => registrar.RegisterService(new TransientServiceDescriptor(serviceType, key, new ScriptableServiceActivator(activator)));

    /// <summary>
    /// Registers a lazy singleton service with the specified activator.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create the singleton instance.</param>
    public static void RegisterLazySingletonService(this IServiceRegistrar registrar, Type serviceType, object key, ScriptableServiceActivator.Activator activator) => registrar.RegisterService(new LazySingletonServiceDescriptor(serviceType, key, new ScriptableServiceActivator(activator)));

    /// <summary>
    /// Registers a scoped service with the specified activator.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterScopedService<TService>(this IServiceRegistrar registrar, object key, IServiceInstanceActivator activator) where TService : class => registrar.RegisterScopedService(typeof(TService), key, activator);

    /// <summary>
    /// Registers a transient service with the specified activator.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterTransientService<TService>(this IServiceRegistrar registrar, object key, IServiceInstanceActivator activator) where TService : class => registrar.RegisterTransientService(typeof(TService), key, activator);

    /// <summary>
    /// Registers a lazy singleton service with the specified activator.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create the singleton instance.</param>
    public static void RegisterLazySingletonService<TService>(this IServiceRegistrar registrar, object key, IServiceInstanceActivator activator) where TService : class => registrar.RegisterLazySingletonService(typeof(TService), key, activator);

    /// <summary>
    /// Registers a weakly-referenced singleton service instance.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterWeakSingletonService<TService>(this IServiceRegistrar registrar, object key, TService instance) where TService : class => registrar.RegisterWeakSingletonService(typeof(TService), key, instance);

    /// <summary>
    /// Registers a strongly-referenced singleton service instance.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterSingletonService<TService>(this IServiceRegistrar registrar, object key, TService instance) where TService : class => registrar.RegisterSingletonService(typeof(TService), key, instance);

    /// <summary>
    /// Registers a scoped service with the specified activator.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterScopedService(this IServiceRegistrar registrar, Type serviceType, object key, IServiceInstanceActivator activator) => registrar.RegisterService(new ScopedServiceDescriptor(serviceType, key, activator));

    /// <summary>
    /// Registers a transient service with the specified activator.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create service instances.</param>
    public static void RegisterTransientService(this IServiceRegistrar registrar, Type serviceType, object key, IServiceInstanceActivator activator) => registrar.RegisterService(new TransientServiceDescriptor(serviceType, key, activator));

    /// <summary>
    /// Registers a lazy singleton service with the specified activator.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="activator">The activator that will create the singleton instance.</param>
    public static void RegisterLazySingletonService(this IServiceRegistrar registrar, Type serviceType, object key, IServiceInstanceActivator activator) => registrar.RegisterService(new LazySingletonServiceDescriptor(serviceType, key, activator));

    /// <summary>
    /// Registers a weakly-referenced singleton service instance.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterWeakSingletonService(this IServiceRegistrar registrar, object key, object instance) => registrar.RegisterWeakSingletonService(instance.GetType(), key, instance);

    /// <summary>
    /// Registers a strongly-referenced singleton service instance.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterSingletonService(this IServiceRegistrar registrar, object key, object instance) => registrar.RegisterSingletonService(instance.GetType(), key, instance);

    /// <summary>
    /// Registers a weakly-referenced singleton service instance.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterWeakSingletonService(this IServiceRegistrar registrar, Type serviceType, object key, object instance) => registrar.RegisterService(new WeakSingletonServiceDescriptor(serviceType, key, instance));

    /// <summary>
    /// Registers a strongly-referenced singleton service instance.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="instance">The service instance to register.</param>
    public static void RegisterSingletonService(this IServiceRegistrar registrar, Type serviceType, object key, object instance) => registrar.RegisterService(new SingletonServiceDescriptor(serviceType, key, instance));
}