using SDI.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

/// <summary>
/// Service accessor that provides access to a singleton service instance.
/// </summary>
public sealed class SingletonServiceAccessor(ServiceId id, object instance) : ServiceAccessorBase(id)
{
    /// <inheritdoc/>
    public override object Access(IServiceProvider provider, ServiceId requestedId) => instance;
}
