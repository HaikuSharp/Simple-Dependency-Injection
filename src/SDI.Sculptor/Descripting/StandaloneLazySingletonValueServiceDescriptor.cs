using SDI.Abstraction;
using SDI.Accessing.Lazy.Scoping;
using SDI.Activating;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

/// <summary>
/// Descriptor for a standalone service with lazy singleton lifetime that uses the default parameterless constructor.
/// The instance is created on first request using the default constructor and then reused for subsequent requests.
/// </summary>
/// <typeparam name="TImplementation">The implementation type, which must have a parameterless constructor.</typeparam>
public readonly struct StandaloneLazySingletonValueServiceDescriptor<TImplementation>(Type serviceType, object key) : IValueServiceDescriptor<StandaloneLazySingletonValueServiceDescriptor<TImplementation>> where TImplementation : class, new()
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType;

    /// <inheritdoc/>
    public object Key => key;

    /// <summary>
    /// Creates a service accessor for this descriptor.
    /// </summary>
    /// <returns>A <see cref="LazySingletonServiceAccessor"/> that uses the default standalone activator for <typeparamref name="TImplementation"/>.</returns>
    public IServiceAccessor CreateAccessor() => new LazySingletonServiceAccessor(ServiceId.FromDescriptor(this), StandaloneServiceActivator<TImplementation>.Default);

    /// <inheritdoc/>
    public StandaloneLazySingletonValueServiceDescriptor<TImplementation> WithId(Type type, object key) => new(type, key);
}