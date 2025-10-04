using SDI.Abstraction;

namespace SDI.Accessing.Lazy.Scoping;

/// <summary>
/// Service accessor that provides lazy-initialized singleton behavior,
/// creating and caching the instance on first access and reusing it for all subsequent requests.
/// </summary>
public sealed class LazySingletonServiceAccessor(ServiceId id, IServiceInstanceActivator activator) : ScopedServiceAccessor(id, activator)
{
    protected override IServiceScopedProvider GetScope(IServiceScopedProvider provider, ServiceId requestedId, ServiceId accessId) => provider.IsRoot ? provider : provider.Root;
}