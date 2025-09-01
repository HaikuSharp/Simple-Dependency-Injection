using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy;

/// <summary>
/// Service accessor that creates a new service instance for each request (transient lifetime).
/// </summary>
public sealed class TransientServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    /// <inheritdoc/>
    protected override object Access(IServiceProvider provider, ServiceId requestedId, ServiceId accessId) => CreateInstance(requestedId, provider);
}