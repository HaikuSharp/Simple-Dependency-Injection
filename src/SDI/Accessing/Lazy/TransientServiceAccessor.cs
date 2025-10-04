using SDI.Abstraction;
using IServiceScopedProvider = SDI.Abstraction.IServiceScopedProvider;

namespace SDI.Accessing.Lazy;

/// <summary>
/// Service accessor that creates a new service instance for each request (transient lifetime).
/// </summary>
public sealed class TransientServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    /// <inheritdoc/>
    protected override object Access(IServiceScopedProvider provider, ServiceId requestedId, ServiceId accessId) => CreateInstance(requestedId, provider);
}