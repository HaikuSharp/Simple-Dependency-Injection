using SDI.Abstraction;
using SDI.Sculptor.Descripting;
using SDI.Sculptor.Extensions;
using System;

namespace SDI.Sculptor.Scoping;

/// <summary>
/// Represents a service registration session that allows configuring service descriptors.
/// </summary>
/// <typeparam name="TRegistrar">The type of the service registrar.</typeparam>
public readonly struct Registration<TRegistrar>(TRegistrar registrar) where TRegistrar : class, IServiceRegistrar
{
    private readonly TRegistrar m_Registrar = registrar;

    /// <summary>
    /// Begins a type registration with no specific type.
    /// </summary>
    /// <returns>A type registration for configuring the service descriptor.</returns>
    public TypeRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter() => Enter(null);

    /// <summary>
    /// Begins a type registration with the specified type.
    /// </summary>
    /// <param name="type">The service type to register.</param>
    /// <returns>A type registration for configuring the service descriptor.</returns>
    public TypeRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter(Type type) => Enter(type, null);

    /// <summary>
    /// Begins a type registration with the specified type and key.
    /// </summary>
    /// <param name="type">The service type to register.</param>
    /// <param name="key">The service key to register.</param>
    /// <returns>A type registration for configuring the service descriptor.</returns>
    public TypeRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter(Type type, object key) => new(this, m_Registrar, type.AsDescriptor(key));

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