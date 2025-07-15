using SDI.Abstraction;

namespace SDI.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceDescriptor"/> implementations.
/// </summary>
public static class ServiceDescriptorExtensions
{
    /// <summary>
    /// Gets the <see cref="ServiceId"/> that uniquely identifies the service described by this descriptor.
    /// </summary>
    /// <param name="descriptor">The service descriptor.</param>
    /// <returns>
    /// A <see cref="ServiceId"/> containing both the service type and registration key.
    /// </returns>
    public static ServiceId GetId(this IServiceDescriptor descriptor) => ServiceId.FromDescriptor(descriptor);
}