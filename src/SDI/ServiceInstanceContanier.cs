using SDI.Abstraction;
using SDI.Exceptions;
using Sugar.Object.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDI;

public class ServiceInstanceContanier : IServiceInstanceContainer
{
    private readonly List<ServiceInstance> m_Instances = [];

    public bool HasInstance(ServiceId id) => m_Instances.Any(i => i.Id.Equals(id));

    public object GetInstance(ServiceId id) => InternalGetInstance(id)?.Instance;

    public bool TryGetInstance(ServiceId id, out object instance)
    {
        instance = GetInstance(id);
        return instance is not null;
    }

    public object Create(ServiceId id, object instance)
    {
        ServiceInstanceAlreadyAddedException.ThrowIfContains(this, id);
        m_Instances.Add(new(id, instance));
        return instance;
    }

    public void Dispose()
    {
        DisposeAll();
        GC.SuppressFinalize(this);
    }

    public void Dispose(ServiceId id) => m_Instances.RemoveAll(i => DisposePredicate(i, id)).Forget();

    public void DisposeAll()
    {
        List<ServiceInstance> instances = m_Instances;
        foreach(ServiceInstance instance in instances) instance.Dispose();
        instances.Clear();
    }

    private ServiceInstance InternalGetInstance(ServiceId id) => m_Instances.FirstOrDefault(i => i.Id.Equals(id));

    private static bool DisposePredicate(ServiceInstance instance, ServiceId id)
    {
        if(!instance.Id.Equals(id)) return false;
        instance.Dispose();
        return true;
    }

    private class ServiceInstance(ServiceId id, object instance) : IDisposable
    {
        internal ServiceId Id => id;

        internal object Instance => instance;

        public void Dispose()
        {
            if(instance is null) return;
            if(instance is not IDisposable disposable) return;
            disposable.Dispose();
        }
    }
}