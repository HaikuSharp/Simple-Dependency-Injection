using SDI.Abstraction;
using System;

namespace SDI;

public class ServiceDescriptor(Type serviceType, Type implementationType, Type lifeTimeType, object key) : IServiceDescriptor
{
    public Type ServiceType => serviceType;

    public Type ImplementationType => implementationType;

    public Type LifeTimeType => lifeTimeType;

    public object Key => key;

    public static IServiceDescriptor Create<TService, TImplementation, TLifeTime>() where TImplementation : TService where TLifeTime : IServiceLifeTime => 
        Create<TService, TImplementation, TLifeTime>(null);

    public static IServiceDescriptor Create<TService, TImplementation, TLifeTime>(object key) where TImplementation : TService where TLifeTime : IServiceLifeTime => 
        new ServiceDescriptor(typeof(TService), typeof(TImplementation), typeof(TLifeTime), key);
}
