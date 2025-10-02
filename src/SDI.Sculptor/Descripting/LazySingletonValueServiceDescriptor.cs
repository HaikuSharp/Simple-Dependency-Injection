using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

public readonly struct LazySingletonValueServiceDescriptor(Type serviceType, object key, IServiceInstanceActivator activator) : ILazyValueServiceDescriptor<LazySingletonValueServiceDescriptor>
{
    public Type ServiceType => serviceType;

    public object Key => key;

    public IServiceInstanceActivator Activator => activator;

    public IServiceAccessor CreateAccessor() => new LazySingletonServiceAccessor(ServiceId.FromDescriptor(this), Activator);

    public LazySingletonValueServiceDescriptor WithId(Type type, object key) => new(type, key, Activator);

    public LazySingletonValueServiceDescriptor WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key, activator); 
}
