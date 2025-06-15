using SDI.Abstraction;
using System;

namespace SDI;

public class ServiceDescriptor(Type serviceType, Type implementationType, Type lifeTimeType, object key) : IServiceDescriptor
{
    public Type ServiceType => serviceType;

    public Type ImplementationType => implementationType;

    public Type LifeTimeType => lifeTimeType;

    public object Key => key;
}
