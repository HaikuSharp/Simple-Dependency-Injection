using SDI.Abstraction;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

public readonly struct ValueServiceDescriptorTemplate(Type serviceType, object key) : IValueServiceDescriptor<ValueServiceDescriptorTemplate>
{
    public Type ServiceType => serviceType;

    public object Key => key;

    public IServiceAccessor CreateAccessor() => throw new NotImplementedException("You cannot create an accessor from a discriptor template. You must define a service lifetime.");

    public ValueServiceDescriptorTemplate WithKey(object key) => WithId(ServiceType, key);

    public ValueServiceDescriptorTemplate WithType(Type type) => WithId(type, Key);

    public ValueServiceDescriptorTemplate WithId(Type type, object key) => new(type, key);

    public static ValueServiceDescriptorTemplate FromDescriptor<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key);
}
