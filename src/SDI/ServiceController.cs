using SDI.Abstraction;
using SDI.Accessing;
using SDI.Exceptions;
using SDI.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI;

/// <summary>
/// Base class for service controllers that manage service registration and scope creation.
/// </summary>
public class ServiceController : IServiceController
{
    /// <summary>
    /// Delegate for service registration/unregistration events.
    /// </summary>
    /// <param name="id">The service identifier being registered or unregistered.</param>
    public delegate void ServiceRegisterHandler(ServiceId id);

    /// <summary>
    /// Default service key used for fundamental service registrations.
    /// </summary>
    public const string DEFAULT_SERVICE_KEY = "default";
    private readonly List<IServiceAccessor> m_Accessors;
    private readonly ServiceProvider m_RootScopeProvider;

    /// <summary>
    /// Initialize new <see cref="ServiceController"/>
    /// </summary>
    public ServiceController()
    {
        m_Accessors = [];
        m_RootScopeProvider = InternalCreateScope(Id);
    }

    /// <inheritdoc/>
    public ScopeId Id => ScopeId.Default;

    /// <summary>
    /// Event raised when a new service is registered.
    /// </summary>
    public event ServiceRegisterHandler OnServiceRegistered;

    /// <summary>
    /// Event raised when a service is unregistered.
    /// </summary>
    public event ServiceRegisterHandler OnServiceUnregistered;

    /// <summary>
    /// Creates and initializes a new service controller of the specified type.
    /// </summary>
    /// <typeparam name="TController">
    /// The type of controller to create, must inherit from <see cref="ServiceController"/> and have a parameterless constructor.
    /// </typeparam>
    /// <returns>
    /// An initialized <typeparamref name="TController"/> instance with default services registered.
    /// </returns>
    public static TController Create<TController>() where TController : ServiceController, new()
    {
        TController controller = new();
        controller.SetupDefaultServices();
        return controller;
    }

    /// <inheritdoc/>
    public bool IsRegistered(ServiceId id) => m_Accessors.Any(a => a.CanAccess(id));

    /// <inheritdoc/>
    public void RegisterService<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor
    {
        ServiceId id = ServiceId.FromDescriptor(descriptor);
        InvalidServiceTypeException.ThrowIfTypeIsNull(id);
        ServiceAlreadyRegisteredException.ThrowIfRegistered(this, id);
        RegisterAccessor(descriptor.CreateAccessor());
        OnServiceRegistered?.Invoke(id);
    }

    /// <inheritdoc/>
    public void UnregisterService(ServiceId id)
    {
        var accessors = m_Accessors;

        for(int i = accessors.Count - 1; i >= 0; i--)
        {
            var accessor = accessors[i];
            if(!accessor.CanAccess(id)) continue;
            accessors.RemoveAt(i);
            OnServiceUnregistered?.Invoke(id);
        }
    }

    /// <inheritdoc/>
    public IServiceProvider CreateScope(ScopeId id) => m_RootScopeProvider.GetService<IServiceProvider>(id) ?? InternalCreateScope(id);

    /// <inheritdoc/>
    public bool IsImplemented(ServiceId id) => m_RootScopeProvider.IsImplemented(id);

    /// <inheritdoc/>
    public IEnumerable GetServices(ServiceId id) => m_RootScopeProvider.GetServices(id);

    /// <inheritdoc/>
    public object GetService(ServiceId id) => m_RootScopeProvider.GetService(id);

    /// <inheritdoc/>
    public object GetService(Type serviceType) => m_RootScopeProvider.GetService(serviceType);

    /// <inheritdoc/>
    public void Dispose()
    {
        m_RootScopeProvider.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Sets up default services that should be available in all containers.
    /// </summary>
    protected virtual void SetupDefaultServices() => RegisterWeakInstance<IServiceController>(DEFAULT_SERVICE_KEY, this);

    /// <summary>
    /// Registers a strongly-referenced singleton service instance.
    /// </summary>
    /// <typeparam name="TInstance">The service type.</typeparam>
    /// <param name="id">The service identifier.</param>
    /// <param name="instance">The service instance to register.</param>
    protected void RegisterInstance<TInstance>(object id, TInstance instance) => RegisterAccessor(new SingletonServiceAccessor(ServiceId.From<TInstance>(id), instance));

    /// <summary>
    /// Registers a weakly-referenced singleton service instance.
    /// </summary>
    /// <typeparam name="TInstance">The service type.</typeparam>
    /// <param name="id">The service identifier.</param>
    /// <param name="instance">The service instance to register.</param>
    protected void RegisterWeakInstance<TInstance>(object id, TInstance instance) => RegisterAccessor(new WeakSingletonServiceAccessor(ServiceId.From<TInstance>(id), instance));

    /// <summary>
    /// Unregisters a service instance by its identifier.
    /// </summary>
    /// <typeparam name="TInstance">The service type.</typeparam>
    /// <param name="id">The service identifier.</param>
    protected void UnregisterInstance<TInstance>(object id) => UnregisterService(ServiceId.From<TInstance>(id));

    internal void RegisterAccessor(IServiceAccessor accessor) => m_Accessors.Add(accessor);

    private object GetService(ServiceId id, IServiceProvider provider) => m_Accessors.FirstOrDefault(a => a.CanAccess(id))?.Access(provider, id);

    private IEnumerable GetServices(ServiceId id, IServiceProvider provider) => m_Accessors.Where(a => a.CanAccess(id)).Select(a => a.Access(provider, id));

    private ServiceProvider InternalCreateScope(ScopeId id)
    {
        ServiceProvider provider = new(id, this);
        provider.InternalInitialize();
        return provider;
    }

    private sealed class ServiceProvider(ScopeId scopeId, ServiceController controller) : IServiceProvider
    {
        private readonly WeakReference<ServiceController> m_WeakController = new(controller);
        private readonly ServiceContainer m_Container = new(scopeId);

        public ScopeId Id => m_Container.Id;

        private ServiceController Controller => m_WeakController.TryGetTarget(out var controller) ? controller : throw new ObjectDisposedException(nameof(controller), "Operation unavailable. The specified Controller has been disposed.");

        public bool IsImplemented(ServiceId id) => Controller.IsRegistered(id);

        public object GetService(Type serviceType) => GetService(ServiceId.FromType(serviceType));

        public object GetService(ServiceId id) => Controller.GetService(id, this);

        public IEnumerable GetServices(ServiceId id) => Controller.GetServices(id, this);

        public IServiceProvider CreateScope(ScopeId id) => Controller.CreateScope(id);

        public void Dispose()
        {
            InternalDeinitialize();
            m_Container.Dispose();
        }

        internal void InternalInitialize()
        {
            var controller = Controller;

            if(controller is null) return;

            var scopeId = Id;

            controller.RegisterWeakInstance<IServiceInstanceContainer>(scopeId, m_Container);
            controller.RegisterWeakInstance<IServiceProvider>(scopeId, this);
            controller.RegisterWeakInstance<IServiceController>(scopeId, controller);

            controller.OnServiceUnregistered += m_Container.Dispose;
        }

        internal void InternalDeinitialize()
        {
            var controller = Controller;
            if(controller is null) return;
            var scopeId = Id;

            controller.UnregisterInstance<IServiceInstanceContainer>(scopeId);
            controller.UnregisterInstance<IServiceProvider>(scopeId);
            controller.UnregisterInstance<IServiceController>(scopeId);

            controller.OnServiceUnregistered -= m_Container.Dispose;
        }

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

            public object GetInstance(ServiceId id) => m_Instances.FirstOrDefault(i => id == i.Id).Instance;

            public void Dispose(ServiceId id)
            {
                var instances = m_Instances;

                for(int i = instances.Count - 1; i >= 0; i--)
                {
                    var serviceInstance = instances[i];
                    if(serviceInstance.Id != id) continue;
                    instances.RemoveAt(i);
                    serviceInstance.Dispose();
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
