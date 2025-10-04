using SDI.Abstraction;
using SDI.Exceptions;
using SDI.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SDI;

/// <summary>
/// Base class for service controllers that manage service registration and scope creation.
/// </summary>
public class ServiceController : IServiceController, IServiceAccessProvider
{
    /// <summary>
    /// Default service key used for fundamental service registrations.
    /// </summary>
    public const string DEFAULT_SERVICE_KEY = "default";

    private static ServiceId ServiceProviderId => ServiceId.From<Abstraction.IServiceProvider>(DEFAULT_SERVICE_KEY);

    private readonly List<IServiceAccessor> m_Accessors = [];
    private readonly ServiceScopedProvider<ServiceController> m_RootScopeProvider;

    IServiceController Abstraction.IServiceProvider.Controller => this;

    /// <summary>
    /// Initialize new <see cref="ServiceController"/>
    /// </summary>
    public ServiceController() => m_RootScopeProvider = InternalCreateRootScope();

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
        var id = descriptor.GetId();

        InvalidServiceIdException.ThrowIfTypeIsNull(id);
        ServiceAlreadyRegisteredException.ThrowIfRegistered(this, id);

        InternalRegisterService(descriptor);
    }

    /// <inheritdoc/>
    public void UnregisterService(ServiceId id)
    {
        ServiceNotRegisteredException.ThrowIfNotRegistered(this, id);
        InternalUnregisterService(id);
    }

    /// <inheritdoc/>
    public Abstraction.IServiceProvider CreateScope() => InternalCreateScope();

    public bool IsImplemented(ServiceId id) => m_RootScopeProvider.IsImplemented(id);

    public IEnumerable GetServices(ServiceId id) => m_RootScopeProvider.GetServices(id);

    public object GetService(ServiceId id) => m_RootScopeProvider.GetService(id);

    public object GetService(Type serviceType) => m_RootScopeProvider.GetService(serviceType);

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

        InternalRegisterAccessor(new SelfServiceAccessor());
    }

    /// <summary>
    /// Unregisters a service instance by its identifier.
    /// </summary>
    /// <typeparam name="TInstance">The service type.</typeparam>
    /// <param name="id">The service identifier.</param>
    protected void UnregisterInstance<TInstance>(object id) => UnregisterService(ServiceId.From<TInstance>(id));

    private ServiceScopedProvider<ServiceController> InternalCreateRootScope() => InternalCreateScope(null);

    private ServiceScopedProvider<ServiceController> InternalCreateScope() => InternalCreateScope(m_RootScopeProvider);

    private ServiceScopedProvider<ServiceController> InternalCreateScope(IServiceScopedProvider root) => new ServiceScopedProvider<ServiceController>(this, root).RegisterProviderInOwner();

    private void InternalRegisterService<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor => InternalRegisterAccessor(descriptor.CreateAccessor());

    private void InternalRegisterAccessor(IServiceAccessor accessor) => m_Accessors.Add(accessor);

    private void InternalUnregisterService(ServiceId id) => _ = m_Accessors.RemoveAll(a => a.CanAccess(id));

    object IServiceAccessProvider.GetService(ServiceId id, IServiceScopedProvider provider) => m_Accessors.FirstOrDefault(a => a.CanAccess(id))?.Access(provider, id);

    IEnumerable IServiceAccessProvider.GetServices(ServiceId id, IServiceScopedProvider provider) => m_Accessors.Where(a => a.CanAccess(id)).Select(a => a.Access(provider, id));

    private sealed class SelfServiceAccessor : IServiceAccessor
    {
        public bool CanAccess(ServiceId requestedId) => requestedId == ServiceProviderId;

        public object Access(IServiceScopedProvider provider, ServiceId requestedId) => provider.Container.GetInstance(ServiceProviderId);
    }

    private sealed class ServiceScopedProvider<TOwner>(TOwner owner, IServiceScopedProvider root) : IServiceScopedProvider where TOwner : class, IServiceController, IServiceAccessProvider
    {
        private readonly TOwner m_Controller = owner;
        private readonly ServiceContainer m_Owner = new();

        bool IServiceScopedProvider.IsRoot => root is null;

        IServiceScopedProvider IServiceScopedProvider.Root => root;

        IServiceController Abstraction.IServiceProvider.Controller => m_Controller;

        IServiceInstanceContainer IServiceScopedProvider.Container => m_Owner;

        public bool IsImplemented(ServiceId id) => m_Controller.IsRegistered(id);

        public object GetService(Type serviceType) => GetService(ServiceId.FromType(serviceType));

        public object GetService(ServiceId id) => m_Controller.GetService(id, this);

        public IEnumerable GetServices(ServiceId id) => m_Controller.GetServices(id, this);

        public Abstraction.IServiceProvider CreateScope() => m_Controller.CreateScope();

        public void Dispose()
        {
            var container = m_Owner;

            // We prevent a cyclical call to Dispose.
            // Because we register the current provider with the container.
            container.Remove(ServiceProviderId);
            container.Dispose();
        }

        internal ServiceScopedProvider<TOwner> RegisterProviderInOwner()
        {
            _ = m_Owner.Create(ServiceProviderId, this);
            return this;
        }

        private sealed class ServiceContainer : IServiceInstanceContainer
        {
            private readonly Dictionary<ServiceId, ServiceInstance> m_Instances = [];

            public bool HasInstance(ServiceId id) => m_Instances.ContainsKey(id);

            public object Create(ServiceId id, object instance)
            {
                ServiceInstanceAlreadyAddedException.ThrowIfContains(this, id);
                m_Instances.Add(id, new(id, instance));
                return instance;
            }

            public object GetInstance(ServiceId id) => m_Instances[id].Instance;

            public void Remove(ServiceId id) => _ = m_Instances.Remove(id);

            public void Dispose(ServiceId id)
            {
                var instances = m_Instances;

                if(!instances.TryGetValue(id, out var instance)) return;

                instance.Dispose();
                _ = instances.Remove(id);
            }

            public void Dispose()
            {
                var instances = m_Instances;

                foreach(var instance in instances.Values) instance.Dispose();

                instances.Clear();
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
