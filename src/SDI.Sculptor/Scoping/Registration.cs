using SDI.Abstraction;
using SDI.Sculptor.Descripting;
using SDI.Sculptor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDI.Sculptor.Scoping;

/// <summary>
/// Represents a service registration session that allows configuring service descriptors.
/// </summary>
/// <typeparam name="TRegistrar">The type of the service registrar.</typeparam>
public readonly struct Registration<TRegistrar> where TRegistrar : class, IServiceRegistrar
{
    private readonly TRegistrar m_Registrar;

    internal Registration(TRegistrar registrar) => m_Registrar = registrar;

    /// <summary>
    /// Begins a range registration for the specified collection of types.
    /// Creates descriptor registrations for each type in the collection.
    /// </summary>
    /// <param name="types">The collection of service types to register.</param>
    /// <returns>A range descriptor registration for the specified types.</returns>
    public RangeDescriptorRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter(IEnumerable<Type> types) => new(this, m_Registrar, types.Select((Func<Type, DiscriptorRegistration<TRegistrar, ValueServiceDescriptorTemplate>>)Enter));

    /// <summary>
    /// Begins a discriptor registration with no specific type.
    /// </summary>
    /// <returns>A discriptor registration for configuring the service descriptor.</returns>
    public DiscriptorRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter() => Enter((Type)null);

    /// <summary>
    /// Begins a discriptor registration with the specified type.
    /// </summary>
    /// <param name="type">The service type to register.</param>
    /// <returns>A discriptor registration for configuring the service descriptor.</returns>
    public DiscriptorRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter(Type type) => Enter(type, null);

    /// <summary>
    /// Begins a discriptor registration with the specified type and key.
    /// </summary>
    /// <param name="type">The service type to register.</param>
    /// <param name="key">The service key to register.</param>
    /// <returns>A discriptor registration for configuring the service descriptor.</returns>
    public DiscriptorRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter(Type type, object key) => new(this, m_Registrar, type.AsDescriptor(key));

    /// <summary>
    /// Ends the registration session and returns the registrar.
    /// </summary>
    /// <returns>The service registrar instance.</returns>
    public TRegistrar End() => m_Registrar;

    /// <summary>
    /// Discards the registration without committing any changes.
    /// </summary>
    public void Forget() { }
}