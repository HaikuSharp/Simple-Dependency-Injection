using SDI.Abstraction;
using SDI.Descripting.Lazy;
using SDI.Reflection.Activating;
using System;

namespace SDI.Reflection.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceRegistrar"/> to simplify registration of reflection-based services.
/// </summary>
public static class ReflectServiceRegistrarExtensions
{
    /// <summary>
    /// Registers a scoped service with its implementation type using reflection-based activation.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterScopedService<TService, TImplementation>(this IServiceRegistrar registrar, object key) where TService : class where TImplementation : class, TService => registrar.RegisterScopedService(typeof(TService), key, typeof(TImplementation));

    /// <summary>
    /// Registers a transient service with its implementation type using reflection-based activation.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterTransientService<TService, TImplementation>(this IServiceRegistrar registrar, object key) where TService : class where TImplementation : class, TService => registrar.RegisterTransientService(typeof(TService), key, typeof(TImplementation));

    /// <summary>
    /// Registers a lazy singleton service with its implementation type using reflection-based activation.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterLazySingletonService<TService, TImplementation>(this IServiceRegistrar registrar, object key) where TService : class where TImplementation : class, TService => registrar.RegisterLazySingletonService(typeof(TService), key, typeof(TImplementation));

    /// <summary>
    /// Registers a scoped service with its implementation type using reflection-based activation.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    public static void RegisterScopedService(this IServiceRegistrar registrar, Type serviceType, object key, Type implementationType) => registrar.RegisterService(new ScopedServiceDescriptor(serviceType, key, GetServiceActivator(implementationType)));

    /// <summary>
    /// Registers a transient service with its implementation type using reflection-based activation.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    public static void RegisterTransientService(this IServiceRegistrar registrar, Type serviceType, object key, Type implementationType) => registrar.RegisterService(new TransientServiceDescriptor(serviceType, key, GetServiceActivator(implementationType)));

    /// <summary>
    /// Registers a lazy singleton service with its implementation type using reflection-based activation.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    public static void RegisterLazySingletonService(this IServiceRegistrar registrar, Type serviceType, object key, Type implementationType) => registrar.RegisterService(new LazySingletonServiceDescriptor(serviceType, key, GetServiceActivator(implementationType)));

    /// <summary>
    /// Gets the appropriate service activator based on the implementation type.
    /// </summary>
    /// <param name="serviceImplementationType">The service implementation type.</param>
    /// <returns>
    /// A <see cref="GenericServiceActivator"/> for open generic types,
    /// or a <see cref="DefaultConstructorServiceActivator"/> for concrete types.
    /// </returns>
    private static IServiceInstanceActivator GetServiceActivator(Type serviceImplementationType) => serviceImplementationType.IsGenericType && serviceImplementationType.ContainsGenericParameters ? new GenericServiceActivator(serviceImplementationType) : new DefaultConstructorServiceActivator(serviceImplementationType);
}