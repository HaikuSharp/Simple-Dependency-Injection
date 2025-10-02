using SDI.Abstraction;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Scoping;

/// <summary>
/// Represents a type-specific service registration that allows configuring service descriptors.
/// </summary>
/// <typeparam name="TRegistrar">The type of the service registrar.</typeparam>
/// <typeparam name="TDescriptor">The type of the service descriptor.</typeparam>
public readonly struct TypeRegistration<TRegistrar, TDescriptor>(Registration<TRegistrar> registration, TRegistrar registrar, TDescriptor descriptor) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor>
{
    private readonly Registration<TRegistrar> m_Registration = registration;
    private readonly TRegistrar m_Registrar = registrar;
    private readonly TDescriptor m_Descriptor = descriptor;

    /// <summary>
    /// Transforms the current descriptor using the specified function.
    /// </summary>
    /// <typeparam name="TCastDescriptor">The target descriptor type.</typeparam>
    /// <param name="func">The function to transform the descriptor.</param>
    /// <returns>A new type registration with the transformed descriptor.</returns>
    public TypeRegistration<TRegistrar, TCastDescriptor> WithDescriptor<TCastDescriptor>(Func<TDescriptor, TCastDescriptor> func) where TCastDescriptor : IValueServiceDescriptor<TCastDescriptor> => new(m_Registration, m_Registrar, func(m_Descriptor));

    /// <summary>
    /// Completes the type registration and returns to the parent registration session.
    /// </summary>
    /// <returns>The parent registration session.</returns>
    public Registration<TRegistrar> Exit()
    {
        m_Registrar.RegisterService(m_Descriptor);
        return m_Registration;
    }
}