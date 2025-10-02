using SDI.Abstraction;
using SDI.Sculptor.Descripting;
using SDI.Sculptor.Extensions;
using System;

namespace SDI.Sculptor.Scoping;

public readonly struct Registration<TRegistrar>(TRegistrar registrar) where TRegistrar : class, IServiceRegistrar
{
    private readonly TRegistrar m_Registrar = registrar;

    public TypeRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter() => Enter(null);

    public TypeRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter(Type type) => Enter(type, null);

    public TypeRegistration<TRegistrar, ValueServiceDescriptorTemplate> Enter(Type type, object key) => new(this, m_Registrar, type.AsDescriptor(key));

    public TRegistrar End() => m_Registrar;

    public void Forget() { }
}
