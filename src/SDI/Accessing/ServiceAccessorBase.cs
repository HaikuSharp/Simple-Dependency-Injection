using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

/// <summary>
/// Base class for service accessors that provides common service matching functionality.
/// </summary>
public abstract class ServiceAccessorBase(ServiceId accessId) : IServiceAccessor
{
    /// <summary>
    /// Determines whether this accessor can provide the requested service.
    /// </summary>
    /// <param name="requestedId">The service identifier being requested.</param>
    public bool CanAccess(ServiceId requestedId) => CanAccess(requestedId, accessId);

    /// <inheritdoc/>
    public abstract object Access(IServiceProvider provider, ServiceId requestedId);

    /// <summary>
    /// Determines whether this accessor can provide the requested service.
    /// </summary>
    /// <param name="requestedId">The service identifier being requested.</param>
    /// <param name="accessId">The service identifier being providing.</param>
    protected virtual bool CanAccess(ServiceId requestedId, ServiceId accessId) => requestedId == accessId
}
