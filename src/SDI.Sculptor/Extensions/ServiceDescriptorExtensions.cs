using SDI.Abstraction;
using SDI.Activating;
using SDI.Sculptor.Descripting;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Extensions;

/// <summary>
/// Provides extension methods for service descriptors.
/// </summary>
public static class ServiceDescriptorExtensions
{
    /// <summary>
    /// Creates a service descriptor template from a type with no key.
    /// </summary>
    /// <param name="type">The service type.</param>
    /// <returns>A new service descriptor template.</returns>
    public static ValueServiceDescriptorTemplate AsDescriptor(this Type type) => type.AsDescriptor(null);

    /// <summary>
    /// Creates a service descriptor template from a type with an optional key.
    /// </summary>
    /// <param name="type">The service type.</param>
    /// <param name="key">The service key.</param>
    /// <returns>A new service descriptor template.</returns>
    public static ValueServiceDescriptorTemplate AsDescriptor(this Type type, object key) => new(type, key);

    /// <summary>
    /// Creates a new descriptor with no key.
    /// </summary>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A new descriptor instance with no key.</returns>
    public static TDescriptor WithoutKey<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IValueServiceDescriptor<TDescriptor> => descriptor.WithKey(null);

    /// <summary>
    /// Creates a new descriptor with the specified key.
    /// </summary>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="key">The service key.</param>
    /// <returns>A new descriptor instance with the specified key.</returns>
    public static TDescriptor WithKey<TDescriptor>(this TDescriptor descriptor, object key) where TDescriptor : IValueServiceDescriptor<TDescriptor> => descriptor.WithId(descriptor.ServiceType, key);

    /// <summary>
    /// Creates a new descriptor with the specified type.
    /// </summary>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="type">The service type.</param>
    /// <returns>A new descriptor instance with the specified type.</returns>
    public static TDescriptor WithType<TDescriptor>(this TDescriptor descriptor, Type type) where TDescriptor : IValueServiceDescriptor<TDescriptor> => descriptor.WithId(type, descriptor.Key);

    /// <summary>
    /// Converts a descriptor to a template descriptor.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A template descriptor with the same type and key.</returns>
    public static ValueServiceDescriptorTemplate AsTemplate<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => ValueServiceDescriptorTemplate.FromDescriptor(descriptor);

    /// <summary>
    /// Converts a descriptor to a singleton descriptor with no instance.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A singleton descriptor with the same type and key.</returns>
    public static SingletonValueServiceDescriptor AsSingleton<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsSingleton(null);

    /// <summary>
    /// Converts a descriptor to a weak singleton descriptor with no instance.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A weak singleton descriptor with the same type and key.</returns>
    public static WeakSingletonValueServiceDescriptor AsWeakSingleton<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsWeakSingleton(null);

    /// <summary>
    /// Converts a descriptor to a lazy singleton descriptor with no activator.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A lazy singleton descriptor with the same type and key.</returns>
    public static LazySingletonValueServiceDescriptor AsLazySingleton<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsLazySingleton((IServiceInstanceActivator)null);

    /// <summary>
    /// Converts a descriptor to a transient descriptor with no activator.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A transient descriptor with the same type and key.</returns>
    public static TransientValueServiceDescriptor AsTransient<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsTransient((IServiceInstanceActivator)null);

    /// <summary>
    /// Converts a descriptor to a scoped descriptor with no activator.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A scoped descriptor with the same type and key.</returns>
    public static ScopedValueServiceDescriptor AsScoped<TDescriptor>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor => descriptor.AsScoped((IServiceInstanceActivator)null);

    /// <summary>
    /// Converts a descriptor to a singleton descriptor with the specified instance.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="instance">The singleton instance.</param>
    /// <returns>A singleton descriptor with the same type and key and the specified instance.</returns>
    public static SingletonValueServiceDescriptor AsSingleton<TDescriptor>(this TDescriptor descriptor, object instance) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, instance);

    /// <summary>
    /// Converts a descriptor to a weak singleton descriptor with the specified instance.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="instance">The weak singleton instance.</param>
    /// <returns>A weak singleton descriptor with the same type and key and the specified instance.</returns>
    public static WeakSingletonValueServiceDescriptor AsWeakSingleton<TDescriptor>(this TDescriptor descriptor, object instance) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, instance);

    /// <summary>
    /// Converts a descriptor to a lazy singleton descriptor with the specified activator.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="activator">The service instance activator.</param>
    /// <returns>A lazy singleton descriptor with the same type and key and the specified activator.</returns>
    public static LazySingletonValueServiceDescriptor AsLazySingleton<TDescriptor>(this TDescriptor descriptor, IServiceInstanceActivator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, activator);

    /// <summary>
    /// Converts a descriptor to a transient descriptor with the specified activator.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="activator">The service instance activator.</param>
    /// <returns>A transient descriptor with the same type and key and the specified activator.</returns>
    public static TransientValueServiceDescriptor AsTransient<TDescriptor>(this TDescriptor descriptor, IServiceInstanceActivator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, activator);

    /// <summary>
    /// Converts a descriptor to a scoped descriptor with the specified activator.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="activator">The service instance activator.</param>
    /// <returns>A scoped descriptor with the same type and key and the specified activator.</returns>
    public static ScopedValueServiceDescriptor AsScoped<TDescriptor>(this TDescriptor descriptor, IServiceInstanceActivator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, activator);

    /// <summary>
    /// Converts a descriptor to a lazy singleton descriptor with the specified scriptable activator.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="activator">The scriptable activator function.</param>
    /// <returns>A lazy singleton descriptor with the same type and key and the specified activator.</returns>
    public static LazySingletonValueServiceDescriptor AsLazySingleton<TDescriptor>(this TDescriptor descriptor, ScriptableServiceActivator.Activator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, new ScriptableServiceActivator(activator));

    /// <summary>
    /// Converts a descriptor to a transient descriptor with the specified scriptable activator.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="activator">The scriptable activator function.</param>
    /// <returns>A transient descriptor with the same type and key and the specified activator.</returns>
    public static TransientValueServiceDescriptor AsTransient<TDescriptor>(this TDescriptor descriptor, ScriptableServiceActivator.Activator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, new ScriptableServiceActivator(activator));

    /// <summary>
    /// Converts a descriptor to a scoped descriptor with the specified scriptable activator.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <param name="activator">The scriptable activator function.</param>
    /// <returns>A scoped descriptor with the same type and key and the specified activator.</returns>
    public static ScopedValueServiceDescriptor AsScoped<TDescriptor>(this TDescriptor descriptor, ScriptableServiceActivator.Activator activator) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key, new ScriptableServiceActivator(activator));

    /// <summary>
    /// Converts a descriptor to a standalone lazy singleton descriptor.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type with parameterless constructor.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A standalone lazy singleton descriptor with the same type and key.</returns>
    public static LazySingletonValueServiceDescriptor AsStandaloneLazySingleton<TDescriptor, TImplementation>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor where TImplementation : class, new() => new(descriptor.ServiceType, descriptor.Key, StandaloneServiceActivator<TImplementation>.Default);

    /// <summary>
    /// Converts a descriptor to a standalone transient descriptor.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type with parameterless constructor.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A standalone transient descriptor with the same type and key.</returns>
    public static TransientValueServiceDescriptor AsStandaloneTransient<TDescriptor, TImplementation>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor where TImplementation : class, new() => new(descriptor.ServiceType, descriptor.Key, StandaloneServiceActivator<TImplementation>.Default);

    /// <summary>
    /// Converts a descriptor to a standalone scoped descriptor.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type with parameterless constructor.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A standalone scoped descriptor with the same type and key.</returns>
    public static ScopedValueServiceDescriptor AsStandaloneScoped<TDescriptor, TImplementation>(this TDescriptor descriptor) where TDescriptor : IServiceDescriptor where TImplementation : class, new() => new(descriptor.ServiceType, descriptor.Key, StandaloneServiceActivator<TImplementation>.Default);
}