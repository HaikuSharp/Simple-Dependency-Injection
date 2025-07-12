using SDI.Abstraction;
using SDI.Activating;
using SDI.Activating.Constructing;
using SDI.Descripting;
using System;
using System.Linq;

namespace SDI.Extensions;

public static class ServiceControllerExtensions
{
    public static void RegisterScopedService<TService, TImplementation>(this IServiceController controller, object key) where TService : class where TImplementation : class, TService => controller.RegisterScopedService(typeof(TService), key, typeof(TImplementation));

    public static void RegisterTransientService<TService, TImplementation>(this IServiceController controller, object key) where TService : class where TImplementation : class, TService => controller.RegisterTransientService(typeof(TService), key, typeof(TImplementation));

    public static void RegisterLazySingletonService<TService, TImplementation>(this IServiceController controller, object key) where TService : class where TImplementation : class, TService => controller.RegisterLazySingletonService(typeof(TService), key, typeof(TImplementation));

    public static void RegisterScopedService(this IServiceController controller, Type serviceType, object key, Type implementationType) => controller.RegisterService(new LazyCurrentScopedServiceDescriptor(serviceType, key, GetServiceActivator(implementationType)));

    public static void RegisterTransientService(this IServiceController controller, Type serviceType, object key, Type implementationType) => controller.RegisterService(new TransientServiceDescriptor(serviceType, key, GetServiceActivator(implementationType)));

    public static void RegisterLazySingletonService(this IServiceController controller, Type serviceType, object key, Type implementationType) => controller.RegisterService(new LazyInstanceServiceDescriptor(serviceType, key, GetServiceActivator(implementationType)));

    public static void RegisterScopedService<TService>(this IServiceController controller, object key, IServiceInstanceActivator activator) where TService : class => controller.RegisterScopedService(typeof(TService), key, activator);

    public static void RegisterTransientService<TService>(this IServiceController controller, object key, IServiceInstanceActivator activator) where TService : class => controller.RegisterTransientService(typeof(TService), key, activator);

    public static void RegisterLazySingletonService<TService>(this IServiceController controller, object key, IServiceInstanceActivator activator) where TService : class => controller.RegisterLazySingletonService(typeof(TService), key, activator);

    public static void RegisterWeakSingletonService<TService>(this IServiceController controller, object key, TService instance) where TService : class => controller.RegisterWeakSingletonService(typeof(TService), key, instance);

    public static void RegisterSingletonService<TService>(this IServiceController controller, object key, TService instance) where TService : class => controller.RegisterSingletonService(typeof(TService), key, instance);

    public static void RegisterScopedService(this IServiceController controller, Type serviceType, object key, IServiceInstanceActivator activator) => controller.RegisterService(new LazyCurrentScopedServiceDescriptor(serviceType, key, activator));

    public static void RegisterTransientService(this IServiceController controller, Type serviceType, object key, IServiceInstanceActivator activator) => controller.RegisterService(new TransientServiceDescriptor(serviceType, key, activator));

    public static void RegisterLazySingletonService(this IServiceController controller, Type serviceType, object key, IServiceInstanceActivator activator) => controller.RegisterService(new LazyInstanceServiceDescriptor(serviceType, key, activator));

    public static void RegisterWeakSingletonService(this IServiceController controller, object key, object instance) => controller.RegisterWeakSingletonService(instance.GetType(), key, instance);

    public static void RegisterSingletonService(this IServiceController controller, object key, object instance) => controller.RegisterSingletonService(instance.GetType(), key, instance);

    public static void RegisterWeakSingletonService(this IServiceController controller, Type serviceType, object key, object instance) => controller.RegisterService(new WeakInstanceServiceDescriptor(serviceType, key, instance));

    public static void RegisterSingletonService(this IServiceController controller, Type serviceType, object key, object instance) => controller.RegisterService(new InstanceServiceDescriptor(serviceType, key, instance));

    private static IServiceInstanceActivator GetServiceActivator(Type type)
    {
        var constructor = type.GetConstructors().OrderBy(c => c.GetParameters().Length).FirstOrDefault();
        if(constructor is null) return null;
        ServiceConstructor serviceConstructor = new(constructor);
        return serviceConstructor.Parameters.Count is 0 ? new EmptyConstructorServiceActivator(serviceConstructor) : new ConstructorServiceActivator(serviceConstructor);
    }
}