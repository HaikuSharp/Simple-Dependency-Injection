﻿using SDI.Abstraction;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Accessing;

/// <summary>
/// Service accessor that maintains a weak reference to a singleton service instance,
/// allowing it to be garbage collected when no other strong references exist.
/// </summary>
public sealed class WeakSingletonServiceAccessor(ServiceId id, object instance) : ServiceAccessorBase(id)
{
    private readonly WeakReference<object> m_WeakInstance = new(instance);

    /// <inheritdoc/>
    public override object Access(IServiceProvider provider, ServiceId requestedId) => m_WeakInstance.TryGetTarget(out object instanceRef) ? instanceRef : null;
}
