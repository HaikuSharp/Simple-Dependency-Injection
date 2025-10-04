using SDI.Abstraction;

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
    public object Access(IServiceScopedProvider provider, ServiceId requestedId) => Access(provider, requestedId, accessId);

    /// <summary>
    /// Determines whether this accessor can provide the requested service.
    /// </summary>
    /// <param name="provider">The service provider used for dependency resolution.</param>
    /// <param name="requestedId">The service identifier to access.</param>
    /// <param name="accessId">The service identifier being providing.</param>
    protected abstract object Access(IServiceScopedProvider provider, ServiceId requestedId, ServiceId accessId);

    /// <summary>
    /// Determines whether this accessor can provide the requested service.
    /// </summary>
    /// <param name="requestedId">The service identifier being requested.</param>
    /// <param name="accessId">The service identifier being providing.</param>
    protected virtual bool CanAccess(ServiceId requestedId, ServiceId accessId) => requestedId == accessId;
}
