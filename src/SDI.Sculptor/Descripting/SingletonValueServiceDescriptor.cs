using SDI.Abstraction;
using SDI.Accessing;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

public readonly struct SingletonValueServiceDescriptor(Type serviceType, object key, object instance) : IInstanceValueServiceDescriptor<SingletonValueServiceDescriptor>
{
    public Type ServiceType => serviceType ?? instance?.GetType();

    public object Key => key;

    public object Instance => instance;

    public IServiceAccessor CreateAccessor() => new SingletonServiceAccessor(ServiceId.FromDescriptor(this), instance);

    public SingletonValueServiceDescriptor WithKey(object key) => WithId(ServiceType, key);

    public SingletonValueServiceDescriptor WithType(Type type) => WithId(type, Key);

    public SingletonValueServiceDescriptor WithId(Type type, object key) => new(type, key, Instance);

    public SingletonValueServiceDescriptor WithInstance(object instance) => new(ServiceType, Key, instance);
}
