using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy;

/// <summary>
/// Base class for lazy-initialized service accessors that defer instance creation until access.
/// </summary>
public abstract class LazyServiceAccessorBase(ServiceId id, IServiceInstanceActivator activator) : ServiceAccessorBase(id)
{
    /// <summary>
    /// Creates a new service instance using the configured activator.
    /// </summary>
    /// <param name="requestedId">The service identifier being activated.</param>
    /// <param name="provider">The service provider for dependency resolution.</param>
    /// <returns>The newly created service instance.</returns>
    protected object CreateInstance(ServiceId requestedId, IServiceProvider provider) => activator.Activate(requestedId, provider);

    /// <inheritdoc/>
    protected override bool CanAccess(ServiceId requestedId, ServiceId accessId) => base.CanAccess(requestedId, accessId) || (requestedId.IsGeneric && requestedId.IsClosedGeneric && accessId.IsGeneric && !accessId.IsClosedGeneric && requestedId.GenericDefinition == accessId);
}