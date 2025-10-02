using SDI.Abstraction;
using SDI.Sculptor.Descripting.Abstraction;
using System;

namespace SDI.Sculptor.Descripting;

/// <summary>
/// Represents a template service descriptor that doesn't have a defined lifetime.
/// Must be converted to a specific lifetime descriptor before use.
/// </summary>
public readonly struct ValueServiceDescriptorTemplate(Type serviceType, object key) : IValueServiceDescriptor<ValueServiceDescriptorTemplate>
{
    /// <inheritdoc/>
    public Type ServiceType => serviceType;

    /// <inheritdoc/>
    public object Key => key;

    /// <summary>
    /// Throws a <see cref="NotImplementedException"/> since templates cannot create accessors.
    /// </summary>
    /// <returns>This method always throws an exception.</returns>
    /// <exception cref="NotImplementedException">Always thrown since templates cannot create accessors.</exception>
    public IServiceAccessor CreateAccessor() => throw new NotImplementedException("You cannot create an accessor from a descriptor template. You must define a service lifetime.");

    /// <inheritdoc/>
    public ValueServiceDescriptorTemplate WithId(Type type, object key) => new(type, key);

    /// <summary>
    /// Creates a template descriptor from any service descriptor.
    /// </summary>
    /// <typeparam name="TDescriptor">The source descriptor type.</typeparam>
    /// <param name="descriptor">The source descriptor.</param>
    /// <returns>A new template descriptor with the same type and key.</returns>
    public static ValueServiceDescriptorTemplate FromDescriptor<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor => new(descriptor.ServiceType, descriptor.Key);
}