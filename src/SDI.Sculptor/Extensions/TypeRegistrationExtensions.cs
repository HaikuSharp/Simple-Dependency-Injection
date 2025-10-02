using SDI.Abstraction;
using SDI.Activating;
using SDI.Sculptor.Descripting;
using SDI.Sculptor.Descripting.Abstraction;
using SDI.Sculptor.Scoping;
using System;

namespace SDI.Sculptor.Extensions;

public static class TypeRegistrationExtensions
{
    public static TypeRegistration<TRegistrar, TDescriptor> WithType<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, Type type) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithType(type));

    public static TypeRegistration<TRegistrar, TDescriptor> WithKey<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, object key) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithKey(key));

    public static TypeRegistration<TRegistrar, TDescriptor> WithId<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, Type type, object key) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithId(type, key));

    public static TypeRegistration<TRegistrar, TDescriptor> WithInstance<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, object instance) where TRegistrar : class, IServiceRegistrar where TDescriptor : IInstanceValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithInstance(instance));

    public static TypeRegistration<TRegistrar, TDescriptor> WithActivator<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, IServiceInstanceActivator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : ILazyValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.WithActivator(activator));

    public static TypeRegistration<TRegistrar, ValueServiceDescriptorTemplate> AsTemplate<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsTemplate());

    public static TypeRegistration<TRegistrar, SingletonValueServiceDescriptor> AsSingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, object instance) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsSingleton(instance));

    public static TypeRegistration<TRegistrar, WeakSingletonValueServiceDescriptor> AsWeakSingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, object instance) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsWeakSingleton(instance));

    public static TypeRegistration<TRegistrar, LazySingletonValueServiceDescriptor> AsLazySingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, IServiceInstanceActivator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsLazySingleton(activator));

    public static TypeRegistration<TRegistrar, ScopedValueServiceDescriptor> AsScoped<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, IServiceInstanceActivator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsScoped(activator));

    public static TypeRegistration<TRegistrar, TransientValueServiceDescriptor> AsTransient<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, IServiceInstanceActivator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsTransient(activator));

    public static TypeRegistration<TRegistrar, LazySingletonValueServiceDescriptor> AsLazySingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, ScriptableServiceActivator.Activator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsLazySingleton(activator));

    public static TypeRegistration<TRegistrar, ScopedValueServiceDescriptor> AsScoped<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, ScriptableServiceActivator.Activator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsScoped(activator));

    public static TypeRegistration<TRegistrar, TransientValueServiceDescriptor> AsTransient<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration, ScriptableServiceActivator.Activator activator) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.WithDescriptor(d => d.AsTransient(activator));

    public static TypeRegistration<TRegistrar, LazySingletonValueServiceDescriptor> AsLazySingleton<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.AsLazySingleton((IServiceInstanceActivator)null);

    public static TypeRegistration<TRegistrar, ScopedValueServiceDescriptor> AsScoped<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.AsScoped((IServiceInstanceActivator)null);

    public static TypeRegistration<TRegistrar, TransientValueServiceDescriptor> AsTransient<TRegistrar, TDescriptor>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.AsTransient((IServiceInstanceActivator)null);

    public static TypeRegistration<TRegistrar, StandaloneLazySingletonValueServiceDescriptor<TImplementation>> AsStandaloneLazySingleton<TRegistrar, TDescriptor, TImplementation>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> where TImplementation : class, new() => registration.WithDescriptor(d => d.AsStandaloneLazySingleton<TDescriptor, TImplementation>());

    public static TypeRegistration<TRegistrar, StandaloneScopedValueServiceDescriptor<TImplementation>> AsStandaloneScoped<TRegistrar, TDescriptor, TImplementation>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> where TImplementation : class, new() => registration.WithDescriptor(d => d.AsStandaloneScoped<TDescriptor, TImplementation>());

    public static TypeRegistration<TRegistrar, StandaloneTransientValueServiceDescriptor<TImplementation>> AsStandaloneTransient<TRegistrar, TDescriptor, TImplementation>(this TypeRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> where TImplementation : class, new() => registration.WithDescriptor(d => d.AsStandaloneTransient<TDescriptor, TImplementation>());
}
