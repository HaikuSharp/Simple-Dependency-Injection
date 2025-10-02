using SDI.Abstraction;
using SDI.Activating;
using SDI.Sculptor.Descripting;
using System;

namespace SDI.Sculptor.Extensions;

public static class ServiceDescriptorExtensions
{
    public static ValueServiceDescriptorTemplate AsDescriptor(this Type type) => type.AsDescriptor(null);

    public static ValueServiceDescriptorTemplate AsDescriptor(this Type type, object key) => new(type, key);

    public static ValueServiceDescriptorTemplate AsTemplate<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => ValueServiceDescriptorTemplate.FromDescriptor(descriptor);

    public static SingletonValueServiceDescriptor AsSingleton<TDescriptor>(this TDescriptor descriptor, object instance) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, instance);

    public static WeakSingletonValueServiceDescriptor AsWeakSingleton<TDescriptor>(this TDescriptor descriptor, object instance) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, instance);

    public static LazySingletonValueServiceDescriptor AsLazySingleton<TDescriptor>(this TDescriptor descriptor, IServiceInstanceActivator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, activator);
    
    public static TransientValueServiceDescriptor AsTransient<TDescriptor>(this TDescriptor descriptor, IServiceInstanceActivator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, activator);
    
    public static ScopedValueServiceDescriptor AsScoped<TDescriptor>(this TDescriptor descriptor, IServiceInstanceActivator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, activator);

    public static LazySingletonValueServiceDescriptor AsLazySingleton<TDescriptor>(this TDescriptor descriptor, ScriptableServiceActivator.Activator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, new ScriptableServiceActivator(activator));

    public static TransientValueServiceDescriptor AsTransient<TDescriptor>(this TDescriptor descriptor, ScriptableServiceActivator.Activator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, new ScriptableServiceActivator(activator));

    public static ScopedValueServiceDescriptor AsScoped<TDescriptor>(this TDescriptor descriptor, ScriptableServiceActivator.Activator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, new ScriptableServiceActivator(activator));
}
