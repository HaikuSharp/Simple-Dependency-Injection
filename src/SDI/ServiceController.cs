using SDI.Abstraction;
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
    /// Default service key used for fundamental service registrations.
    /// </summary>
    public const string DEFAULT_SERVICE_KEY = "default";
    private readonly ServiceAccessorProvider m_AccessorProvider;
    private readonly ServiceProvider m_RootScopeProvider;

    /// <summary>
    /// Initialize new <see cref="ServiceController"/>
    /// </summary>
    public ServiceController()
    {
        m_AccessorProvider = new();
        m_RootScopeProvider = InternalCreateScope(Id);
    }

    /// <inheritdoc/>
    public ScopeId Id => ScopeId.Default;

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
    public bool IsRegistered(ServiceId id) => m_AccessorProvider.IsRegistered(id);

    /// <inheritdoc/>
    public void RegisterService<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor
    {
        InvalidServiceIdException.ThrowIfTypeIsNull(descriptor.GetId());
        m_AccessorProvider.RegisterService(descriptor);
    }

    /// <inheritdoc/>
    public void UnregisterService(ServiceId id) => m_AccessorProvider.UnregisterService(id);

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
    protected virtual void SetupDefaultServices() 
    {
        this.RegisterSingletonService<IServiceController>(DEFAULT_SERVICE_KEY, this);
        this.RegisterSingletonService<IServiceRegistrar>(DEFAULT_SERVICE_KEY, this);
    }

    /// <summary>
    /// Unregisters a service instance by its identifier.
    /// </summary>
    /// <typeparam name="TInstance">The service type.</typeparam>
    /// <param name="id">The service identifier.</param>
    protected void UnregisterInstance<TInstance>(object id) => UnregisterService(ServiceId.From<TInstance>(id));

    private ServiceProvider InternalCreateScope(ScopeId id)
    {
        ServiceProvider provider = new(id, m_AccessorProvider);
        provider.InternalInitialize();
        return provider;
    }

    private sealed class ServiceAccessorProvider : IServiceRegistrar, IServiceAccessProvider
    {
        private readonly List<IServiceAccessor> m_Accessors = [];

        public bool IsRegistered(ServiceId id) => m_Accessors.Any(a => a.CanAccess(id));

        public void RegisterService<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor
        {
            ServiceAlreadyRegisteredException.ThrowIfRegistered(this, descriptor.GetId());
            InternalRegisterService(descriptor);
        }

        public void UnregisterService(ServiceId id)
        {
            ServiceNotRegisteredException.ThrowIfNotRegistered(this, id);
            InternalUnregisterService(id);
        }

        public object GetService(ServiceId id, IServiceProvider provider) => m_Accessors.FirstOrDefault(a => a.CanAccess(id))?.Access(provider, id);

        public IEnumerable GetServices(ServiceId id, IServiceProvider provider) => m_Accessors.Where(a => a.CanAccess(id)).Select(a => a.Access(provider, id));

        private void InternalRegisterService<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor => m_Accessors.Add(descriptor.CreateAccessor());

        private void InternalUnregisterService(ServiceId id) => _ = m_Accessors.RemoveAll(a => a.CanAccess(id));
    }

    private sealed class ServiceProvider(ScopeId scopeId, ServiceAccessorProvider accessorProvider) : IServiceProvider
    {
        private readonly WeakReference<ServiceAccessorProvider> m_WeakProvider = new(accessorProvider);
        private readonly ServiceContainer m_Container = new();

        public ScopeId Id => scopeId;

        private ServiceAccessorProvider Provider => m_WeakProvider.TryGetTarget(out var controller) ? controller : throw new ObjectDisposedException(nameof(controller), "Operation unavailable. The specified Controller has been disposed.");

        public bool IsImplemented(ServiceId id) => Provider.IsRegistered(id);

        public object GetService(Type serviceType) => GetService(ServiceId.FromType(serviceType));

        public object GetService(ServiceId id) => Provider.GetService(id, this);

        public IEnumerable GetServices(ServiceId id) => Provider.GetServices(id, this);

        public void Dispose()
        {
            InternalDeinitialize();
            m_Container.Dispose();
        }

        internal void InternalInitialize()
        {
            var provider = Provider;
            var scopeId = Id;

            provider.RegisterWeakSingletonService<IServiceInstanceContainer>(scopeId, m_Container);
            provider.RegisterWeakSingletonService<IServiceProvider>(scopeId, this);
        }

        internal void InternalDeinitialize()
        {
            var provider = Provider;
            var scopeId = Id;

            provider.UnregisterService<IServiceInstanceContainer>(scopeId);
            provider.UnregisterService<IServiceProvider>(scopeId);
        }

        private sealed class ServiceContainer : IServiceInstanceContainer
        {
            private readonly List<ServiceInstance> m_Instances = [];

            public bool HasInstance(ServiceId id) => m_Instances.Any(i => id == i.Id);

            public object Create(ServiceId id, object instance)
            {
                ServiceInstanceAlreadyAddedException.ThrowIfContains(this, id);
                m_Instances.Add(new(id, instance));
                return instance;
            }

            public object GetInstance(ServiceId id) => m_Instances.FirstOrDefault(i => id == i.Id).Instance;

            public void Dispose(ServiceId id) => m_Instances.RemoveAll(i => DisposeInstanceIfNeeded(i, id));

            public void Dispose()
            {
                m_Instances.ForEach(i => i.Dispose());
                m_Instances.Clear();
            }

            private static bool DisposeInstanceIfNeeded(ServiceInstance instance, ServiceId id)
            {
                if(instance.Id != id) return false;
                instance.Dispose();
                return true;
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
