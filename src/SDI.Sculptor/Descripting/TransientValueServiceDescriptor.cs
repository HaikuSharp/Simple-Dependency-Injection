using SDI.Abstraction;
using SDI.Accessing.Lazy;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

public readonly struct TransientValueServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : ILazyValueServiceDescriptor<TransientValueServiceDescriptor>
{
    public Type ServiceType => serviceType;

    public object Key => key;

    public IServiceInstanceActivator Activator => activator;

    public IServiceAccessor CreateAccessor() => new TransientServiceAccessor(ServiceId.FromDescriptor(this), Activator);

    public TransientValueServiceDescriptor WithId(Type type, object key) => new(type, key, Activator);

    public TransientValueServiceDescriptor WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key, activator);
}
