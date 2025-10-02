using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using SDI.Activating;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

/// <summary>
/// Descriptor for a standalone service with scoped lifetime that uses the default parameterless constructor.
/// </summary>
/// <typeparam name="TImplementation">The implementation type, which must have a parameterless constructor.</typeparam>
public readonly struct StandaloneScopedValueServiceDescriptor<TImplementation>(Type serviceType, object key) : IValueServiceDescriptor<StandaloneScopedValueServiceDescriptor<TImplementation>> where TImplementation : class, new()
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType;

    /// <inheritdoc/>
    public object Key => key;

    /// <inheritdoc/>
    public IServiceAccessor CreateAccessor() => new ScopedServiceAccessor(ServiceId.FromDescriptor(this), StandaloneServiceActivator<TImplementation>.Default);

    /// <inheritdoc/>
    public StandaloneScopedValueServiceDescriptor<TImplementation> WithId(Type type, object key) => new(type, key);

    /// <inheritdoc/>
    public StandaloneScopedValueServiceDescriptor<TImplementation> WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key);
}