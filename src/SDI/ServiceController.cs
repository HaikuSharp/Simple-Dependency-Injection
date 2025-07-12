using SDI.Abstraction;
using SDI.Exceptions;
using SDI.Extensions;
using SDI.Resolving;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI;

public class ServiceController : IServiceController
{
    private readonly List<IServiceAccessor> m_Accessors = [];

    public static IServiceController Create()
    {
        ServiceController controller = new();
        controller.SetupDefaultServices();
        return controller;
    }

    public bool IsRegistered(ServiceId id) => m_Accessors.Any(a => a.CanAccess(id));

    public void RegisterService<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor
    {
        ServiceAlreadyRegisteredException.ThrowIfRegistered(this, ServiceId.FromDescriptor(descriptor));
        RegisterAccessor(descriptor.CreateAccessor());
    }

    public void UnregisterService(ServiceId id) => m_Accessors.RemoveAll(a => a.CanAccess(id));

    public IServiceProvider CreateScope(ScopeId id)
    {
        ServiceProvider provider = new(id, this);
        provider.InternalInitialize();
        return provider;
    }

    protected virtual void SetupDefaultServices()
    {
        this.RegisterInstance<IServiceController>("default", this);
        this.RegisterInstance<IServiceDependencyResolver<ParameterInfo>>("default", new ServiceDependencyParameterResolver());
    }

    internal object GetService(ServiceId id, IServiceProvider provider) => m_Accessors.FirstOrDefault(a => a.CanAccess(id))?.Access(provider);

    internal IEnumerable GetServices(ServiceId id, IServiceProvider provider) => m_Accessors.Where(a => a.CanAccess(id)).Select(a => a.Access(provider));

    internal void RegisterAccessor(IServiceAccessor accessor) => m_Accessors.Add(accessor);

    private sealed class ServiceProvider(ScopeId id, ServiceController controller) : IServiceProvider
    {
        private readonly WeakReference<ServiceController> m_WeakController = new(controller);

        public ScopeId Id => Container.Id;

        public IServiceController Controller => InternalGetController();

        public IServiceInstanceContainer Container { get; } = new ServiceContainer(id);

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
            var id = Id;

            controller.RegisterInstance<IServiceInstanceContainer>(id, Container);
            controller.RegisterInstance<IServiceProvider>(id, this);
            controller.RegisterInstance<IServiceController>(id, controller);
        }
        
        internal void InternalDeinitialize()
        {
            var controller = InternalGetController();
            var id = Id;

            controller.UnregisterInstance<IServiceInstanceContainer>(id);
            controller.UnregisterInstance<IServiceProvider>(id);
            controller.UnregisterInstance<IServiceController>(id);
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

            public void Dispose(ServiceId id) => m_Instances.RemoveAll(i => DisposePredicate(i, id));

            public void Dispose()
            {
                m_Instances.ForEach(i => i.Dispose());
                m_Instances.Clear();
            }

            private static bool DisposePredicate(ServiceInstance instance, ServiceId id)
            {
                if(!instance.Id.Equals(id)) return false;
                instance.Dispose();
                return true;
            }

            private readonly struct ServiceInstance(ServiceId id, object instance) : IDisposable
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
    }
}
