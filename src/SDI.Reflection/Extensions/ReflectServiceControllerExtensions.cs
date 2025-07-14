using SDI.Abstraction;
using SDI.Descripting.Lazy;
using SDI.Reflection.Activating;
using System;

namespace SDI.Reflection.Extensions;

public static class ReflectServiceControllerExtensions
{
    public static void RegisterScopedService<TService, TImplementation>(this IServiceController controller, object key) where TService : class where TImplementation : class, TService => controller.RegisterScopedService(typeof(TService), key, typeof(TImplementation));

    public static void RegisterTransientService<TService, TImplementation>(this IServiceController controller, object key) where TService : class where TImplementation : class, TService => controller.RegisterTransientService(typeof(TService), key, typeof(TImplementation));

    public static void RegisterLazySingletonService<TService, TImplementation>(this IServiceController controller, object key) where TService : class where TImplementation : class, TService => controller.RegisterLazySingletonService(typeof(TService), key, typeof(TImplementation));

    public static void RegisterScopedService(this IServiceController controller, Type serviceType, object key, Type implementationType) => controller.RegisterService(new ScopedServiceDescriptor(serviceType, key, GetServiceActivator(implementationType)));

    public static void RegisterTransientService(this IServiceController controller, Type serviceType, object key, Type implementationType) => controller.RegisterService(new TransientServiceDescriptor(serviceType, key, GetServiceActivator(implementationType)));

    public static void RegisterLazySingletonService(this IServiceController controller, Type serviceType, object key, Type implementationType) => controller.RegisterService(new LazySingletonServiceDescriptor(serviceType, key, GetServiceActivator(implementationType)));

    private static IServiceInstanceActivator GetServiceActivator(Type serviceImplementationType) => serviceImplementationType.IsGenericType && serviceImplementationType.ContainsGenericParameters ? new GenericServiceActivator(serviceImplementationType) : new DefaultConstructorServiceActivator(serviceImplementationType);
}
