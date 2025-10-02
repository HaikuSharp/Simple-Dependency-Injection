using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using SDI.Activating;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

public readonly struct StandaloneScopedValueServiceDescriptor<TImplementation>(Type serviceType, object key) : IValueServiceDescriptor<StandaloneScopedValueServiceDescriptor<TImplementation>> where TImplementation : class, new()
{
    public Type ServiceType => serviceType;

    public object Key => key;

    public IServiceAccessor CreateAccessor() => new ScopedServiceAccessor(ServiceId.FromDescriptor(this), StandaloneServiceActivator<TImplementation>.Default);

    public StandaloneScopedValueServiceDescriptor<TImplementation> WithId(Type type, object key) => new(type, key);

    public StandaloneScopedValueServiceDescriptor<TImplementation> WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key);
}
