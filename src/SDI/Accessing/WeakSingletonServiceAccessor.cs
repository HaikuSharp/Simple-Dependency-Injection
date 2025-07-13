using SDI.Abstraction;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

public sealed class WeakSingletonServiceAccessor(ServiceId id, object instance) : ServiceAccessorBase(id)
{
    private readonly WeakReference<object> m_WeakInstance = new(instance);

    protected override object Access(IServiceProvider provider, ServiceId id) => m_WeakInstance.TryGetTarget(out object instanceRef) ? instanceRef : null;
}
