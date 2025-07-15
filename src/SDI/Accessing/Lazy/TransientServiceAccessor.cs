using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy;

/// <summary>
/// Service accessor that creates a new service instance for each request (transient lifetime).
/// </summary>
public sealed class TransientServiceAccessor(ServiceId id, IServiceInstanceActivator activator)
    : LazyServiceAccessorBase(id, activator)
{
    /// <summary>
    /// Creates and returns a new service instance for each access request.
    /// </summary>
    /// <param name="provider">The service provider for dependency resolution.</param>
    /// <param name="requestedId">The service identifier being requested.</param>
    /// <returns>A new instance of the service each time it is accessed.</returns>
    public override object Access(IServiceProvider provider, ServiceId requestedId)
        => CreateInstance(requestedId, provider);
}