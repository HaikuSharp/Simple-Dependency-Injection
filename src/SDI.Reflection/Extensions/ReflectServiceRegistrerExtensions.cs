using SDI.Abstraction;
using SDI.Descripting.Lazy;
using SDI.Reflection.Abstraction;
using SDI.Reflection.Activating;
using SDI.Reflection.Resolving;
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
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    public static void RegisterScopedService<TService, TImplementation>(this IServiceRegistrar registrar) where TService : class where TImplementation : class, TService => registrar.RegisterScopedService(typeof(TService), null, typeof(TImplementation));

    /// <summary>
    /// Registers a transient service with its implementation type using reflection-based activation.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    public static void RegisterTransientService<TService, TImplementation>(this IServiceRegistrar registrar) where TService : class where TImplementation : class, TService => registrar.RegisterTransientService(typeof(TService), null, typeof(TImplementation));

    /// <summary>
    /// Registers a lazy singleton service with its implementation type using reflection-based activation.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    public static void RegisterLazySingletonService<TService, TImplementation>(this IServiceRegistrar registrar) where TService : class where TImplementation : class, TService => registrar.RegisterLazySingletonService(typeof(TService), null, typeof(TImplementation));

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
    /// Registers a scoped service with its implementation type using reflection-based activation.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    public static void RegisterScopedService(this IServiceRegistrar registrar, Type serviceType, Type implementationType) => registrar.RegisterScopedService(serviceType, null, implementationType);

    /// <summary>
    /// Registers a transient service with its implementation type using reflection-based activation.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    public static void RegisterTransientService(this IServiceRegistrar registrar, Type serviceType, Type implementationType) => registrar.RegisterTransientService(serviceType, null, implementationType);

    /// <summary>
    /// Registers a lazy singleton service with its implementation type using reflection-based activation.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    public static void RegisterLazySingletonService(this IServiceRegistrar registrar, Type serviceType, Type implementationType) => registrar.RegisterLazySingletonService(serviceType, null, implementationType);

    /// <summary>
    /// Registers a scoped service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterScopedService(this IServiceRegistrar registrar, Type serviceType, object key, Type implementationType, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) => registrar.RegisterService(new ScopedServiceDescriptor(serviceType, key, GetServiceActivator(implementationType, constructorResolver, dependencyResolver)));

    /// <summary>
    /// Registers a transient service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterTransientService(this IServiceRegistrar registrar, Type serviceType, object key, Type implementationType, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) => registrar.RegisterService(new TransientServiceDescriptor(serviceType, key, GetServiceActivator(implementationType, constructorResolver, dependencyResolver)));

    /// <summary>
    /// Registers a lazy singleton service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterLazySingletonService(this IServiceRegistrar registrar, Type serviceType, object key, Type implementationType, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) => registrar.RegisterService(new LazySingletonServiceDescriptor(serviceType, key, GetServiceActivator(implementationType, constructorResolver, dependencyResolver)));

    /// <summary>
    /// Registers a scoped service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterScopedService(this IServiceRegistrar registrar, Type serviceType, Type implementationType, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) => registrar.RegisterScopedService(serviceType, null, implementationType, constructorResolver, dependencyResolver);

    /// <summary>
    /// Registers a transient service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterTransientService(this IServiceRegistrar registrar, Type serviceType, Type implementationType, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) => registrar.RegisterTransientService(serviceType, null, implementationType, constructorResolver, dependencyResolver);

    /// <summary>
    /// Registers a lazy singleton service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="serviceType">The service type to register.</param>
    /// <param name="implementationType">The implementation type that derives from <paramref name="serviceType"/>.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterLazySingletonService(this IServiceRegistrar registrar, Type serviceType, Type implementationType, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) => registrar.RegisterLazySingletonService(serviceType, null, implementationType, constructorResolver, dependencyResolver);

    /// <summary>
    /// Registers a scoped service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterScopedService<TService, TImplementation>(this IServiceRegistrar registrar, object key, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) where TService : class where TImplementation : class, TService => registrar.RegisterScopedService(typeof(TService), key, typeof(TImplementation), constructorResolver, dependencyResolver);

    /// <summary>
    /// Registers a transient service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterTransientService<TService, TImplementation>(this IServiceRegistrar registrar, object key, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) where TService : class where TImplementation : class, TService => registrar.RegisterTransientService(typeof(TService), key, typeof(TImplementation), constructorResolver, dependencyResolver);

    /// <summary>
    /// Registers a lazy singleton service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterLazySingletonService<TService, TImplementation>(this IServiceRegistrar registrar, object key, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) where TService : class where TImplementation : class, TService => registrar.RegisterLazySingletonService(typeof(TService), key, typeof(TImplementation), constructorResolver, dependencyResolver);

    /// <summary>
    /// Registers a scoped service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterScopedService<TService, TImplementation>(this IServiceRegistrar registrar, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) where TService : class where TImplementation : class, TService => registrar.RegisterScopedService(typeof(TService), null, typeof(TImplementation), constructorResolver, dependencyResolver);

    /// <summary>
    /// Registers a transient service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterTransientService<TService, TImplementation>(this IServiceRegistrar registrar, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) where TService : class where TImplementation : class, TService => registrar.RegisterTransientService(typeof(TService), null, typeof(TImplementation), constructorResolver, dependencyResolver);

    /// <summary>
    /// Registers a lazy singleton service with its implementation type using reflection-based activation with custom resolvers.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that derives from <typeparamref name="TService"/>.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="constructorResolver">The constructor resolver to use for service activation.</param>
    /// <param name="dependencyResolver">The dependency resolver to use for service activation.</param>
    public static void RegisterLazySingletonService<TService, TImplementation>(this IServiceRegistrar registrar, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) where TService : class where TImplementation : class, TService => registrar.RegisterLazySingletonService(typeof(TService), null, typeof(TImplementation), constructorResolver, dependencyResolver);

    private static IServiceInstanceActivator GetServiceActivator(Type serviceImplementationType) => GetServiceActivator(serviceImplementationType, ServiceDefaultConstructorResolver.Default, ServiceDependencyResolver.Default);

    private static IServiceInstanceActivator GetServiceActivator(Type serviceImplementationType, IServiceConstructorResolver constructorResolver, IServiceDependencyResolver dependencyResolver) => serviceImplementationType.IsGenericType && serviceImplementationType.ContainsGenericParameters ? new GenericServiceActivator(serviceImplementationType, constructorResolver, dependencyResolver) : new DefaultConstructorServiceActivator(serviceImplementationType, constructorResolver, dependencyResolver);
}