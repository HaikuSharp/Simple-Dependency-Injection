using SDI.Abstraction;
using SDI.Activating;
using SDI.Sculptor.Descripting;
using SDI.Sculptor.Descripting.Abstraction;
using SDI.Sculptor.Scoping;
using System;

namespace SDI.Sculptor.Extensions;

/// <summary>
/// Provides extension methods for type registrations.
/// </summary>
public static class TypeRegistrationExtensions
{
    /// <summary>
    /// Configures the registration with the specified service type.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="type">The service type.</param>
    /// <returns>The updated type registration.</returns>
    public static TypeRegistration<TRegistrar, TDescriptor> WithType<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, Type type) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithType(type));

    /// <summary>
    /// Configures the registration without the service key.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <returns>The updated type registration.</returns>
    public static TypeRegistration<TRegistrar, TDescriptor> WithoutKey<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithoutKey());

    /// <summary>
    /// Configures the registration with the specified service key.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="key">The service key.</param>
    /// <returns>The updated type registration.</returns>
    public static TypeRegistration<TRegistrar, TDescriptor> WithKey<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, object key) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithKey(key));

    /// <summary>
    /// Configures the registration with the specified service type and key.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="type">The service type.</param>
    /// <param name="key">The service key.</param>
    /// <returns>The updated type registration.</returns>
    public static TypeRegistration<TRegistrar, TDescriptor> WithId<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, Type type, object key) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithId(type, key));

    /// <summary>
    /// Configures the registration with the specified service instance.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="instance">The service instance.</param>
    /// <returns>The updated type registration.</returns>
    public static TypeRegistration<TRegistrar, TDescriptor> WithInstance<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, object instance) where TRegistrar : class, IServiceRegistrar where TDescriptor : IInstanceValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithInstance(instance));

    /// <summary>
    /// Configures the registration with the specified service activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="activator">The service instance activator.</param>
    /// <returns>The updated type registration.</returns>
    public static TypeRegistration<TRegistrar, TDescriptor> WithActivator<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, IServiceInstanceActivator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : ILazyValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithActivator(activator));

    /// <summary>
    /// Converts the registration to a template descriptor registration.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <returns>A registration with template descriptor.</returns>
    public static TypeRegistration<TRegistrar, ValueServiceDescriptorTemplate> AsTemplate<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsTemplate());

    /// <summary>
    /// Converts the registration to a singleton descriptor registration with the specified instance.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="instance">The singleton instance.</param>
    /// <returns>A registration with singleton descriptor.</returns>
    public static TypeRegistration<TRegistrar, SingletonValueServiceDescriptor> AsSingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, object instance) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsSingleton(instance));

    /// <summary>
    /// Converts the registration to a weak singleton descriptor registration with the specified instance.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="instance">The weak singleton instance.</param>
    /// <returns>A registration with weak singleton descriptor.</returns>
    public static TypeRegistration<TRegistrar, WeakSingletonValueServiceDescriptor> AsWeakSingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, object instance) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsWeakSingleton(instance));

    /// <summary>
    /// Converts the registration to a lazy singleton descriptor registration with the specified activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="activator">The service instance activator.</param>
    /// <returns>A registration with lazy singleton descriptor.</returns>
    public static TypeRegistration<TRegistrar, LazySingletonValueServiceDescriptor> AsLazySingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, IServiceInstanceActivator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsLazySingleton(activator));

    /// <summary>
    /// Converts the registration to a scoped descriptor registration with the specified activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="activator">The service instance activator.</param>
    /// <returns>A registration with scoped descriptor.</returns>
    public static TypeRegistration<TRegistrar, ScopedValueServiceDescriptor> AsScoped<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, IServiceInstanceActivator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsScoped(activator));

    /// <summary>
    /// Converts the registration to a transient descriptor registration with the specified activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="activator">The service instance activator.</param>
    /// <returns>A registration with transient descriptor.</returns>
    public static TypeRegistration<TRegistrar, TransientValueServiceDescriptor> AsTransient<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, IServiceInstanceActivator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsTransient(activator));

    /// <summary>
    /// Converts the registration to a lazy singleton descriptor registration with the specified scriptable activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="activator">The scriptable activator function.</param>
    /// <returns>A registration with lazy singleton descriptor.</returns>
    public static TypeRegistration<TRegistrar, LazySingletonValueServiceDescriptor> AsLazySingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, ScriptableServiceActivator.Activator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsLazySingleton(activator));

    /// <summary>
    /// Converts the registration to a scoped descriptor registration with the specified scriptable activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="activator">The scriptable activator function.</param>
    /// <returns>A registration with scoped descriptor.</returns>
    public static TypeRegistration<TRegistrar, ScopedValueServiceDescriptor> AsScoped<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, ScriptableServiceActivator.Activator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsScoped(activator));

    /// <summary>
    /// Converts the registration to a transient descriptor registration with the specified scriptable activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <param name="activator">The scriptable activator function.</param>
    /// <returns>A registration with transient descriptor.</returns>
    public static TypeRegistration<TRegistrar, TransientValueServiceDescriptor> AsTransient<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, ScriptableServiceActivator.Activator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsTransient(activator));

    /// <summary>
    /// Converts the registration to a lazy singleton descriptor registration with no activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <returns>A registration with lazy singleton descriptor.</returns>
    public static TypeRegistration<TRegistrar, LazySingletonValueServiceDescriptor> AsLazySingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.AsLazySingleton((IServiceInstanceActivator)null);

    /// <summary>
    /// Converts the registration to a scoped descriptor registration with no activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <returns>A registration with scoped descriptor.</returns>
    public static TypeRegistration<TRegistrar, ScopedValueServiceDescriptor> AsScoped<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.AsScoped((IServiceInstanceActivator)null);

    /// <summary>
    /// Converts the registration to a transient descriptor registration with no activator.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <returns>A registration with transient descriptor.</returns>
    public static TypeRegistration<TRegistrar, TransientValueServiceDescriptor> AsTransient<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.AsTransient((IServiceInstanceActivator)null);

    /// <summary>
    /// Converts the registration to a standalone lazy singleton descriptor registration.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type with parameterless constructor.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <returns>A registration with standalone lazy singleton descriptor.</returns>
    public static TypeRegistration<TRegistrar, StandaloneLazySingletonValueServiceDescriptor<TImplementation>> AsStandaloneLazySingleton<TRegistrar, TDescriptor, TImplementation>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> where TImplementation : class, new() => registration.WithDescriptor(d => d.AsStandaloneLazySingleton<TDescriptor, TImplementation>());

    /// <summary>
    /// Converts the registration to a standalone scoped descriptor registration.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type with parameterless constructor.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <returns>A registration with standalone scoped descriptor.</returns>
    public static TypeRegistration<TRegistrar, StandaloneScopedValueServiceDescriptor<TImplementation>> AsStandaloneScoped<TRegistrar, TDescriptor, TImplementation>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> where TImplementation : class, new() => registration.WithDescriptor(d => d.AsStandaloneScoped<TDescriptor, TImplementation>());

    /// <summary>
    /// Converts the registration to a standalone transient descriptor registration.
    /// </summary>
    /// <typeparam name="TRegistrar">The registrar type.</typeparam>
    /// <typeparam name="TDescriptor">The descriptor type.</typeparam>
    /// <typeparam name="TImplementation">The implementation type with parameterless constructor.</typeparam>
    /// <param name="registration">The type registration.</param>
    /// <returns>A registration with standalone transient descriptor.</returns>
    public static TypeRegistration<TRegistrar, StandaloneTransientValueServiceDescriptor<TImplementation>> AsStandaloneTransient<TRegistrar, TDescriptor, TImplementation>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> where TImplementation : class, new() => registration.WithDescriptor(d => d.AsStandaloneTransient<TDescriptor, TImplementation>());
}