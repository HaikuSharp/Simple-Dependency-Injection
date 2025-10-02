using SDI.Abstraction;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Scoping;

public readonly struct TypeRegistration<TRegistrar, TDescriptor>(Registration<TRegistrar> registration, TRegistrar registrar, TDescriptor descriptor) where TRegistrar : class, IServiceRegistrar where TDescriptor : IValueServiceDescriptor<TDescriptor>
{
    private readonly Registration<TRegistrar> m_Registration = registration;
    private readonly TRegistrar m_Registrar = registrar;
    private readonly TDescriptor m_Descriptor = descriptor;

    public TypeRegistration<TRegistrar, TCastDescriptor> WithDescriptor<TCastDescriptor>(Func<TDescriptor, TCastDescriptor> func) where TCastDescriptor : IValueServiceDescriptor<TCastDescriptor> => new(m_Registration, m_Registrar, func(m_Descriptor));

    public Registration<TRegistrar> Exit()
    {
        m_Registrar.RegisterService(m_Descriptor);
        return m_Registration;
    }
}