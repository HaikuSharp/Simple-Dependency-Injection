using SDI.Abstraction;
using SDI.Exceptions;
using SDI.Extensions;
using SDI.Resolving;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI;

public class ServiceController : IServiceController
{
    public delegate void ServiceRegisterHandler(ServiceId id);

    public const string DEFAULT_SERVICE_KEY = "default";
    private readonly List<IServiceAccessor> m_Accessors = [];

    public event ServiceRegisterHandler OnServiceRegistered;
    public event ServiceRegisterHandler OnServiceUnregistered;

    public static IServiceController Create()
    {
        ServiceController controller = new();
        controller.SetupDefaultServices();
        return controller;
    }

    public bool IsRegistered(ServiceId id) => m_Accessors.Any(a => a.CanAccess(id));

    public void RegisterService<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor
    {
        ServiceId id = ServiceId.FromDescriptor(descriptor);
        ServiceAlreadyRegisteredException.ThrowIfRegistered(this, id);
        RegisterAccessor(descriptor.CreateAccessor());
        OnServiceRegistered?.Invoke(id);
    }

    public void UnregisterService(ServiceId id)
    {
        var accessors = m_Accessors;
        for(int i = 0; i < accessors.Count; i++)
        {
            var accessor = accessors[i];
            if(!accessor.CanAccess(id)) continue;
            accessors.RemoveAt(i);
            OnServiceUnregistered?.Invoke(id);
            break;
        }
    }

    public IServiceProvider CreateScope(ScopeId id)
    {
        ServiceProvider provider = new(id, this);
        provider.InternalInitialize();
        return provider;
    }

    protected virtual void SetupDefaultServices()
    {
        this.RegisterInstance<IServiceController>(DEFAULT_SERVICE_KEY, this);
        this.RegisterInstance<IServiceDependencyResolver>(DEFAULT_SERVICE_KEY, new ServiceDependencyResolver());
    }

    private object GetService(ServiceId id, IServiceProvider provider) => m_Accessors.FirstOrDefault(a => a.CanAccess(id))?.Access(provider);

    private IEnumerable GetServices(ServiceId id, IServiceProvider provider) => m_Accessors.Where(a => a.CanAccess(id)).Select(a => a.Access(provider));

    internal void RegisterAccessor(IServiceAccessor accessor) => m_Accessors.Add(accessor);

    private sealed class ServiceProvider(ScopeId scopeId, ServiceController controller) : IServiceProvider
    {
        private readonly WeakReference<ServiceController> m_WeakController = new(controller);

        public ScopeId Id => Container.Id;

        public IServiceController Controller => InternalGetController();

        public IServiceInstanceContainer Container { get; } = new ServiceContainer(scopeId);

        public bool IsImplemented(ServiceId id) => InternalGetController().IsRegistered(id);

        public object GetService(Type serviceType) => GetService(ServiceId.FromType(serviceType));

        public object GetService(ServiceId id) => InternalGetController().GetService(id, this);

        public IEnumerable GetServices(ServiceId id) => InternalGetController().GetServices(id, this);

        public void Dispose()
        {
            InternalDeinitialize();
            Container.Dispose();
        }

        internal void InternalInitialize()
        {
            var controller = InternalGetController();
            if(controller is null) return;
            var scopeId = Id;

            controller.RegisterInstance(scopeId, Container);
            controller.RegisterInstance<IServiceProvider>(scopeId, this);
            controller.RegisterInstance<IServiceController>(scopeId, controller);

            controller.OnServiceUnregistered += Container.Dispose;
        }

        internal void InternalDeinitialize()
        {
            var controller = InternalGetController();
            if(controller is null) return;
            var scopeId = Id;

            controller.UnregisterInstance<IServiceInstanceContainer>(scopeId);
            controller.UnregisterInstance<IServiceProvider>(scopeId);
            controller.UnregisterInstance<IServiceController>(scopeId);

            controller.OnServiceUnregistered -= Container.Dispose;
        }

        private ServiceController InternalGetController() => m_WeakController.TryGetTarget(out var controllerRef) ? controllerRef : null;

        private sealed class ServiceContainer(ScopeId id) : IServiceInstanceContainer
        {
            public ScopeId Id => id;

            private readonly List<ServiceInstance> m_Instances = [];

            public bool HasInstance(ServiceId id) => m_Instances.Any(i => id == i.Id);

            public object Create(ServiceId id, object instance)
            {
                ServiceInstanceAlreadyAddedException.ThrowIfContains(this, id);
                m_Instances.Add(new(id, instance));
                return instance;
            }

            public object GetIsntance(ServiceId id) => m_Instances.FirstOrDefault(i => id == i.Id).Instance;

            public void Dispose(ServiceId id)
            {
                var instances = m_Instances;
                for(int i = 0; i < instances.Count; i++)
                {
                    var serviceInstance = instances[i];
                    if(serviceInstance.Id != id) continue;
                    serviceInstance.Dispose();
                    instances.RemoveAt(i);
                    break;
                }
            }

            public void Dispose()
            {
                m_Instances.ForEach(i => i.Dispose());
                m_Instances.Clear();
            }

            private readonly struct ServiceInstance(ServiceId id, object instance) : IDisposable
            {
                internal ServiceId Id => id;

                internal object Instance => instance;

                public void Dispose()
                {
                    if(instance is not IDisposable disposable) return;
                    disposable.Dispose();
                }
            }
        }
    }
}
