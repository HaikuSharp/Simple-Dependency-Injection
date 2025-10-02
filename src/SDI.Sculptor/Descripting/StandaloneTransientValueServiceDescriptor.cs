using SDI.Abstraction;
using SDI.Accessing.Lazy;
using SDI.Activating;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

public readonly struct StandaloneTransientValueServiceDescriptor<TImplementation>(Type serviceType, object key) : ILazyValueServiceDescriptor<StandaloneTransientValueServiceDescriptor<TImplementation>> where TImplementation : class, new()
{
    public Type ServiceType => serviceType;

    public object Key => key;

    public IServiceAccessor CreateAccessor() => new TransientServiceAccessor(ServiceId.FromDescriptor(this), StandaloneServiceActivator<TImplementation>.Default);

    public StandaloneTransientValueServiceDescriptor<TImplementation> WithId(Type type, object key) => new(type, key);

    public StandaloneTransientValueServiceDescriptor<TImplementation> WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key);
}