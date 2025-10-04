using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing.Lazy;

/// <summary>
/// Service accessor that provides lazy-initialized singleton behavior,
/// creating and caching the instance on first access and reusing it for all subsequent requests.
/// </summary>
public sealed class LazySingletonServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : LazyServiceAccessorBase(id, activator)
{
    private object m_Instance;

    /// <inheritdoc/>
    protected override object Access(IServiceProvider provider, ServiceId requestedId, ServiceId accessId) => m_Instance ??= CreateInstance(requestedId, provider);
}