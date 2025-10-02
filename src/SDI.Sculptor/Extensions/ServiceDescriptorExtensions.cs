using SDI.Abstraction;
using SDI.Activating;
using SDI.Sculptor.Descripting;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Extensions;

public static class ServiceDescriptorExtensions
{
    public static ValueServiceDescriptorTemplate AsDescriptor(this Type type) => type.AsDescriptor(null);

    public static ValueServiceDescriptorTemplate AsDescriptor(this Type type, object key) => new(type, key);

    public static TDescriptor WithoutKey<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IValueServiceDescriptor<TDescriptor> => descriptor.WithKey(null);

    public static TDescriptor WithKey<TDescriptor>(this TDescriptor descriptor, object key) where TDescriptor : IValueServiceDescriptor<TDescriptor> => descriptor.WithId(descriptor.ServiceType, key);

    public static TDescriptor WithType<TDescriptor>(this TDescriptor descriptor, Type type) where TDescriptor : IValueServiceDescriptor<TDescriptor> => descriptor.WithId(type, descriptor.Key);

    public static ValueServiceDescriptorTemplate AsTemplate<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => ValueServiceDescriptorTemplate.FromDescriptor(descriptor);

    public static SingletonValueServiceDescriptor AsSingleton<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsSingleton(null);

    public static WeakSingletonValueServiceDescriptor AsWeakSingleton<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsWeakSingleton(null);

    public static LazySingletonValueServiceDescriptor AsLazySingleton<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsLazySingleton((IServiceInstanceActivator)null);

    public static TransientValueServiceDescriptor AsTransient<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsTransient((IServiceInstanceActivator)null);

    public static ScopedValueServiceDescriptor AsScoped<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsScoped((IServiceInstanceActivator)null);

    public static SingletonValueServiceDescriptor AsSingleton<TDescriptor>(this TDescriptor descriptor, object instance) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, instance);

    public static WeakSingletonValueServiceDescriptor AsWeakSingleton<TDescriptor>(this TDescriptor descriptor, object instance) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, instance);

    public static LazySingletonValueServiceDescriptor AsLazySingleton<TDescriptor>(this TDescriptor descriptor, IServiceInstanceActivator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, activator);
    
    public static TransientValueServiceDescriptor AsTransient<TDescriptor>(this TDescriptor descriptor, IServiceInstanceActivator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, activator);
    
    public static ScopedValueServiceDescriptor AsScoped<TDescriptor>(this TDescriptor descriptor, IServiceInstanceActivator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, activator);

    public static LazySingletonValueServiceDescriptor AsLazySingleton<TDescriptor>(this TDescriptor descriptor, ScriptableServiceActivator.Activator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, new ScriptableServiceActivator(activator));

    public static TransientValueServiceDescriptor AsTransient<TDescriptor>(this TDescriptor descriptor, ScriptableServiceActivator.Activator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, new ScriptableServiceActivator(activator));

    public static ScopedValueServiceDescriptor AsScoped<TDescriptor>(this TDescriptor descriptor, ScriptableServiceActivator.Activator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, new ScriptableServiceActivator(activator));

    public static StandaloneLazySingletonValueServiceDescriptor<TImplementation> AsStandaloneLazySingleton<TDescriptor, TImplementation>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor where TImplementation : class, new() => new(descriptor.ServiceType, descriptor.Key);

    public static StandaloneTransientValueServiceDescriptor<TImplementation> AsStandaloneTransient<TDescriptor, TImplementation>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor where TImplementation : class, new() => new(descriptor.ServiceType, descriptor.Key);

    public static StandaloneScopedValueServiceDescriptor<TImplementation> AsStandaloneScoped<TDescriptor, TImplementation>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor where TImplementation : class, new() => new(descriptor.ServiceType, descriptor.Key);
}
