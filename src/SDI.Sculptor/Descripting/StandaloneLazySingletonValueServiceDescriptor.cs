using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using SDI.Activating;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

public readonly struct StandaloneLazySingletonValueServiceDescriptor<TImplementation>(Type serviceType, object key) : IValueServiceDescriptor<StandaloneLazySingletonValueServiceDescriptor<TImplementation>> where TImplementation : class, new()
{
    public Type ServiceType => serviceType;

    public object Key => key;

    public IServiceAccessor CreateAccessor() => new LazySingletonServiceAccessor(ServiceId.FromDescriptor(this), StandaloneServiceActivator<TImplementation>.Default);

    public StandaloneLazySingletonValueServiceDescriptor<TImplementation> WithId(Type type, object key) => new(type, key);
}
