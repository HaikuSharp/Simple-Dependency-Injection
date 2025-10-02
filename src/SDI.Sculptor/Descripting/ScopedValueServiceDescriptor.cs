using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

public readonly struct ScopedValueServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : ILazyValueServiceDescriptor<ScopedValueServiceDescriptor>
{
    public Type ServiceType => serviceType;

    public object Key => key;

    public IServiceInstanceActivator Activator => activator;

    public IServiceAccessor CreateAccessor() => new ScopedServiceAccessor(ServiceId.FromDescriptor(this), Activator);

    public ScopedValueServiceDescriptor WithKey(object key) => WithId(ServiceType, key);

    public ScopedValueServiceDescriptor WithType(Type type) => WithId(type, Key);

    public ScopedValueServiceDescriptor WithId(Type type, object key) => new(type, key, Activator);

    public ScopedValueServiceDescriptor WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key, activator);
}
