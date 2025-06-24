using SDI.Abstraction;
using SDI.Exceptions;
using SDI.LifeTimes;
using Sugar.Object.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI;

public class ServiceProvider(IServiceInstanceContainer contanier, IServiceLifeTime lifeTime) : IServiceProvider
{
    private readonly List<IServiceAccessor> m_Accessors = [];

    public IServiceProvider RegisterSelf()
    {
        RegisterSelfService(typeof(IServiceProvider), this);
        RegisterSelfService(typeof(IServiceInstanceContainer), contanier);
        RegisterSelfService(typeof(IServiceLifeTime), lifeTime);
        return this;
    }

    public bool IsImplemented(ServiceId id) => m_Accessors.Any(a => a.CanAccess(id));

    public IEnumerable GetServices(ServiceId id) => m_Accessors.Where(a => a.CanAccess(id)).Select(a => a.Access(this));

    public object GetService(ServiceId id) => m_Accessors.FirstOrDefault(a => a.CanAccess(id))?.Access(this);

    public IServiceProvider RegisterService(IServiceDescriptor descriptor)
    {
        m_Accessors.Add(CreateAccessorAndVerifyRegistration(descriptor));
        return this;
    }

    public IServiceProvider RegisterServices(IEnumerable<IServiceDescriptor> descriptors)
    {
        m_Accessors.AddRange(descriptors.Select(CreateAccessorAndVerifyRegistration));
        return this;
    }

    public IServiceProvider UnregisterService(ServiceId id)
    {
        ServiceNotImplementedException.ThrowIfNotImplemented(this, id);
        m_Accessors.RemoveAll(a => a.CanAccess(id)).Forget();
        contanier.Dispose(id);
        return this;
    }

    public void Dispose()
    {
        contanier.DisposeAll();
        GC.SuppressFinalize(this);
    }

    private void RegisterSelfService(Type serviceType, object instance)
    {
        ServiceDescriptor descriptor = new(serviceType, instance.GetType(), typeof(SingletonServiceLifeTime), "self");
        RegisterService(descriptor).Forget();
        contanier.Create(ServiceId.FromDescriptor(descriptor), instance).Forget();
    }

    private IServiceAccessor CreateAccessorAndVerifyRegistration(IServiceDescriptor descriptor)
    {
        ServiceAlreadyImplementedException.ThrowIfImplemented(this, ServiceId.FromDescriptor(descriptor));
        return lifeTime.CreateAccessor(descriptor);
    }
}
