using SDI.Abstraction;

namespace SDI.Accessing;

/// <summary>
/// Service accessor that provides access to a singleton service instance.
/// </summary>
public sealed class SingletonServiceAccessor(ServiceId id, object instance) : ServiceAccessorBase(id)
{
    /// <inheritdoc/>
    protected override object Access(IServiceScopedProvider provider, ServiceId requestedId, ServiceId accessId) => instance;
}
