using SDI.Abstraction;
using SDI.Sculptor.Descripting;
using SDI.Sculptor.Descripting.Abstraction;
using SDI.Sculptor.Scoping;
using System.Collections.Generic;
using System.Reflection;

namespace SDI.Sculptor.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceRegistrar"/>.
/// </summary>
public static class ServiceRegistrarExtensions
{
    /// <summary>
    /// Begins a new registration session for the specified registrar.
    /// </summary>
    /// <typeparam name="TRegistrar">The type of the service registrar.</typeparam>
    /// <param name="registrar">The service registrar instance.</param>
    /// <returns>A new registration session.</returns>
    public static Registration<TRegistrar> RegistrationBegin<TRegistrar>(this TRegistrar registrar) where TRegistrar : class, IServiceRegistrar => new(registrar);

    /// <summary>
    /// Begins a range registration for all types in the specified assembly.
    /// </summary>
    /// <typeparam name="TRegistrar">The type of the service registrar.</typeparam>
    /// <param name="registration">The registration session.</param>
    /// <param name="assembly">The assembly containing the types to register.</param>
    /// <returns>A range descriptor registration for the assembly types.</returns>
    public static RangeDescriptorRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter<TRegistrar>(this Registration<TRegistrar> registration, Assembly assembly) where TRegistrar : class, IServiceRegistrar => registration.Enter(assembly.GetTypes());

    /// <summary>
    /// Completes the descriptor registration by exiting and discarding the parent registration session.
    /// </summary>
    /// <typeparam name="TRegistrar">The type of the service registrar.</typeparam>
    /// <typeparam name="TDescriptor">The type of the service descriptor.</typeparam>
    /// <param name="registration">The descriptor registration to complete.</param>
    public static void Complite<TRegistrar, TDescriptor>(this DiscriptorRegistration<TRegistrar, TDescriptor> registration) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor> => registration.Exit().Forget();
}