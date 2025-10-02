using SDI.Abstraction;
using SDI.Accessing;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

public readonly struct WeakSingletonValueServiceDescriptor(Type serviceType, object key, object instance) : IInstanceValueServiceDescriptor<WeakSingletonValueServiceDescriptor>
{
    public Type ServiceType => serviceType ?? instance?.GetType();

    public object Key => key;

    public object Instance => instance;

    public IServiceAccessor CreateAccessor() => new WeakSingletonServiceAccessor(ServiceId.FromDescriptor(this), instance);

    public WeakSingletonValueServiceDescriptor WithKey(object key) => WithId(ServiceType, key);

    public WeakSingletonValueServiceDescriptor WithType(Type type) => WithId(type, Key);

    public WeakSingletonValueServiceDescriptor WithId(Type type, object key) => new(type, key, Instance);

    public WeakSingletonValueServiceDescriptor WithInstance(object instance) => new(ServiceType, Key, instance);
}
