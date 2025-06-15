using SDI.Abstraction;
using SDI.Analizers;
using SDI.LifeTimes;
using SDI.LifeTimes.Lazy;
using SDI.Resolve;
using SDI.Resolve.Inject;
using System;
using System.Collections.Generic;
using System.Reflection;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI;

public class ServiceSetup
{
    public virtual IServiceProvider CreateProvider() => new ServiceProvider(CreateInstanceContanier(), CreateRootLifeTime()).RegisterSelf();



    public IServiceDescriptAnalizer<Type> CreateTypeAnalizer() => new InjectTypeServiceDescriptAnalizer(CreateAttributeTypeResolver<Type>(), CreateAttributeKeyResolver<Type>(), CreateAttributeLifeTimeResolver<Type>());
    public virtual IServiceDescriptAnalizer<Assembly> CreateAssemblyAnalizer() => new AssemblyServiceDescriptAnalizer(CreateTypeAnalizer());

    protected virtual IServiceInstanceContanier CreateInstanceContanier() => new ServiceInstanceContanier();

    protected virtual IServiceLifeTime CreateRootLifeTime() => new ServiceLifeTimeManager(CreateServiceLifeTimes());

    protected virtual IEnumerable<IServiceLifeTime> CreateServiceLifeTimes() => [new SingletonServiceLifeTime(), new LazySingletonServiceLifeTime(CreateActivatorResolver()), new TransientServiceLifeTime(CreateActivatorResolver())];

    protected virtual IServiceActivatorResolver CreateActivatorResolver() => new ServiceActivatorResolver(CreateConstructorResolver(), CreateMethodParameterDependencyResolver());

    protected virtual IServiceConstructorResolver CreateConstructorResolver() => new DefaultServiceConstructorResolver();

    protected virtual IServiceDependencyResolver<ParameterInfo> CreateMethodParameterDependencyResolver() => new ServiceMethodParameterDependencyResolver(CreateAttributeTypeResolver<ParameterInfo>(), CreateAttributeKeyResolver<ParameterInfo>());

    protected virtual IServiceTypeResolver<T> CreateAttributeTypeResolver<T>() where T : ICustomAttributeProvider => new ServiceAttributeTypeResolver<T>();

    protected virtual IServiceKeyResolver<T> CreateAttributeKeyResolver<T>() where T : ICustomAttributeProvider => new ServiceAttributeKeyResolver<T>();

    protected virtual IServiceLifeTimeTypeResolver<T> CreateAttributeLifeTimeResolver<T>() where T : ICustomAttributeProvider => new ServiceAttributeLifeTimeTypeResolver<T>();

    protected virtual IServiceDescriptResolver<Type> CreateAttributeTypeDescriptResolver<T>() => new ServiceAttributeDescriptTypeResolver();
}
