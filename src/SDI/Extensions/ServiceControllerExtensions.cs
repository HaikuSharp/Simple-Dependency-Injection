using SDI.Abstraction;
using SDI.Descripting;
using SDI.Descripting.Lazy;
using System;

namespace SDI.Extensions;

public static class ServiceControllerExtensions
{
    public static void RegisterScopedService<TService>(this IServiceController controller, object key, IServiceInstanceActivator activator) where TService : class => controller.RegisterScopedService(typeof(TService), key, activator);

    public static void RegisterTransientService<TService>(this IServiceController controller, object key, IServiceInstanceActivator activator) where TService : class => controller.RegisterTransientService(typeof(TService), key, activator);

    public static void RegisterLazySingletonService<TService>(this IServiceController controller, object key, IServiceInstanceActivator activator) where TService : class => controller.RegisterLazySingletonService(typeof(TService), key, activator);

    public static void RegisterWeakSingletonService<TService>(this IServiceController controller, object key, TService instance) where TService : class => controller.RegisterWeakSingletonService(typeof(TService), key, instance);

    public static void RegisterSingletonService<TService>(this IServiceController controller, object key, TService instance) where TService : class => controller.RegisterSingletonService(typeof(TService), key, instance);

    public static void RegisterScopedService(this IServiceController controller, Type serviceType, object key, IServiceInstanceActivator activator) => controller.RegisterService(new ScopedServiceDescriptor(serviceType, key, activator));

    public static void RegisterTransientService(this IServiceController controller, Type serviceType, object key, IServiceInstanceActivator activator) => controller.RegisterService(new TransientServiceDescriptor(serviceType, key, activator));

    public static void RegisterLazySingletonService(this IServiceController controller, Type serviceType, object key, IServiceInstanceActivator activator) => controller.RegisterService(new LazySingletonServiceDescriptor(serviceType, key, activator));

    public static void RegisterWeakSingletonService(this IServiceController controller, object key, object instance) => controller.RegisterWeakSingletonService(instance.GetType(), key, instance);

    public static void RegisterSingletonService(this IServiceController controller, object key, object instance) => controller.RegisterSingletonService(instance.GetType(), key, instance);

    public static void RegisterWeakSingletonService(this IServiceController controller, Type serviceType, object key, object instance) => controller.RegisterService(new WeakSingletonServiceDescriptor(serviceType, key, instance));

    public static void RegisterSingletonService(this IServiceController controller, Type serviceType, object key, object instance) => controller.RegisterService(new SingletonServiceDescriptor(serviceType, key, instance));

    public static Abstraction.IServiceProvider CreateDefaultScope(this IServiceController controller) => controller.CreateScope(ScopeId.Default);
}