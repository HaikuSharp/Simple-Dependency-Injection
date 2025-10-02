using SDI.Abstraction;
using SDI.Accessing.Lazy;
using SDI.Activating;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

/// <summary>
/// Descriptor for a standalone service with transient lifetime that uses the default parameterless constructor.
/// </summary>
/// <typeparam name="TImplementation">The implementation type, which must have a parameterless constructor.</typeparam>
public readonly struct StandaloneTransientValueServiceDescriptor<TImplementation>(Type serviceType, object key) : ILazyValueServiceDescriptor<StandaloneTransientValueServiceDescriptor<TImplementation>> where TImplementation : class, new()
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType;

    /// <inheritdoc/>
    public object Key => key;

    /// <inheritdoc/>
    public IServiceAccessor CreateAccessor() => new TransientServiceAccessor(ServiceId.FromDescriptor(this), StandaloneServiceActivator<TImplementation>.Default);

    /// <inheritdoc/>
    public StandaloneTransientValueServiceDescriptor<TImplementation> WithId(Type type, object key) => new(type, key);

    /// <inheritdoc/>
    public StandaloneTransientValueServiceDescriptor<TImplementation> WithActivator(IServiceInstanceActivator activator) => new(ServiceType, Key);
}