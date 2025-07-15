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
    /// <returns>
    /// <c>true</c> if:
    /// <list type="bullet">
    /// <item><description>The requested ID matches exactly, or</description></item>
    /// <item><description>The requested ID is a closed generic that matches this open generic definition</description></item>
    /// </list>
    /// </returns>
    public bool CanAccess(ServiceId requestedId) => requestedId == accessId || (requestedId.IsGeneric && requestedId.IsClosedGeneric && accessId.IsGeneric && !accessId.IsClosedGeneric && requestedId.GenericDefinition == accessId);

    /// <inheritdoc/>
    public abstract object Access(IServiceProvider provider, ServiceId requestedId);
}
