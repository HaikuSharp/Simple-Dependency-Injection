using SDI.Abstraction;
using SDI.Sculptor.Descripting.Abstraction;
using SDI.Sculptor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDI.Sculptor.Scoping;

/// <summary>
/// Represents a registration that handles multiple service descriptors in a range/batch operation.
/// Allows configuring and registering multiple service descriptors simultaneously.
/// </summary>
/// <typeparam name="TRegistrar">The type of the service registrar.</typeparam>
/// <typeparam name="TDescriptor">The type of the service descriptor.</typeparam>
public readonly struct RangeDescriptorRegistration<TRegistrar, TDescriptor> where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor>
{
    private readonly Registration<TRegistrar> m_Registration;
    private readonly TRegistrar m_Registrar;
    private readonly IEnumerable<DiscriptorRegistration<TRegistrar, TDescriptor>> m_Registractions;

    internal RangeDescriptorRegistration(Registration<TRegistrar> registration, TRegistrar registrar, IEnumerable<DiscriptorRegistration<TRegistrar, TDescriptor>> registrations)
    {
        m_Registration = registration;
        m_Registrar = registrar;
        m_Registractions = registrations;
    }

    /// <summary>
    /// Transforms all descriptor registrations in the range using the specified selector function.
    /// </summary>
    /// <typeparam name="TCastDescriptor">The target descriptor type.</typeparam>
    /// <param name="selector">The function to transform each descriptor registration.</param>
    /// <returns>A new range descriptor registration with the transformed descriptors.</returns>
    public RangeDescriptorRegistration<TRegistrar, TCastDescriptor> WithDescriptors<TCastDescriptor>(Func<IEnumerable<DiscriptorRegistration<TRegistrar, TDescriptor>>, IEnumerable<DiscriptorRegistration<TRegistrar, TCastDescriptor>>> selector) where TCastDescriptor : IValueServiceDescriptor<TCastDescriptor> => new(m_Registration, m_Registrar, selector(m_Registractions));

    /// <summary>
    /// Completes the range registration by committing all individual descriptor registrations and returns to the parent registration session.
    /// </summary>
    /// <returns>The parent registration session.</returns>
    public Registration<TRegistrar> Exit()
    {
        foreach(var registration in m_Registractions) registration.Complite();
        return m_Registration;
    }
}