using SDI.Abstraction;
using SDI.Configuration.Activating;
using SDI.Extensions;

namespace SDI.Configuration.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceController"/> to simplify registration of configurable services.
/// </summary>
public static class ServiceControllerExtensions
{
    /// <summary>
    /// Registers a scoped configurable service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that must be configurable.</typeparam>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterScopedConfigurableService<TService, TImplementation>(this IServiceController controller, object key) where TService : class where TImplementation : class, TService, IConfigurableService, new() => controller.RegisterScopedService<TService>(key, ConfigurableServiceActivator<TImplementation>.Default);

    /// <summary>
    /// Registers a transient configurable service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that must be configurable.</typeparam>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterTransientConfigurableService<TService, TImplementation>(this IServiceController controller, object key) where TService : class where TImplementation : class, TService, IConfigurableService, new() => controller.RegisterTransientService<TService>(key, ConfigurableServiceActivator<TImplementation>.Default);

    /// <summary>
    /// Registers a lazy singleton configurable service with the specified implementation type.
    /// </summary>
    /// <typeparam name="TService">The service type to register.</typeparam>
    /// <typeparam name="TImplementation">The implementation type that must be configurable.</typeparam>
    /// <param name="controller">The service controller.</param>
    /// <param name="key">The optional service key.</param>
    public static void RegisterLazySingletonConfigurableService<TService, TImplementation>(this IServiceController controller, object key) where TService : class where TImplementation : class, TService, IConfigurableService, new() => controller.RegisterLazySingletonService<TService>(key, ConfigurableServiceActivator<TImplementation>.Default);
}