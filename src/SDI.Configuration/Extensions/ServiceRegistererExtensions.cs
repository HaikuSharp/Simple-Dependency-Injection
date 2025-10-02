using SDI.Abstraction;
using SDI.Configuration.Activating;
using SDI.Extensions;

namespace SDI.Configuration.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceRegistrar"/> to simplify registration of configurable services.
/// </summary>
public static class ServiceRegistererExtensions
{
    /// <summary>
    /// Registers a scoped configurable service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterScopedConfigurableService<TService>(this IServiceRegistrar registrar, object key) where TService : class, IConfigurableService, new() => registrar.RegisterScopedConfigurableService<TService, TService>(key);

    /// <summary>
    /// Registers a transient configurable service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterTransientConfigurableService<TService>(this IServiceRegistrar registrar, object key) where TService : class, IConfigurableService, new() => registrar.RegisterTransientConfigurableService<TService, TService>(key);

    /// <summary>
    /// Registers a lazy singleton configurable service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterLazySingletonConfigurableService<TService>(this IServiceRegistrar registrar, object key) where TService : class, IConfigurableService, new() => registrar.RegisterLazySingletonConfigurableService<TService, TService>(key);

    /// <summary>
    /// Registers a scoped configurable service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that must be configurable.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterScopedConfigurableService<TService, TImplementation>(this IServiceRegistrar registrar, object key) where TService : class where TImplementation : class, TService, IConfigurableService, new() => registrar.RegisterScopedService<TService>(key, ConfigurableServiceActivator<TImplementation>.Default);

    /// <summary>
    /// Registers a transient configurable service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that must be configurable.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterTransientConfigurableService<TService, TImplementation>(this IServiceRegistrar registrar, object key) where TService : class where TImplementation : class, TService, IConfigurableService, new() => registrar.RegisterTransientService<TService>(key, ConfigurableServiceActivator<TImplementation>.Default);

    /// <summary>
    /// Registers a lazy singleton configurable service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that must be configurable.</typeparam>
    /// <param name="registrar">The service registrar.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterLazySingletonConfigurableService<TService, TImplementation>(this IServiceRegistrar registrar, object key) where TService : class where TImplementation : class, TService, IConfigurableService, new() => registrar.RegisterLazySingletonService<TService>(key, ConfigurableServiceActivator<TImplementation>.Default);
}