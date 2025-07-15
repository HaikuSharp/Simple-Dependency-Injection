using System;

namespace SDI.Abstraction;

/// <summary>
/// Represents a service registration descriptor containing service metadata and factory capabilities.
/// </summary>
public interface IServiceDescriptor
{
    /// <summary>
    /// Gets the type of the service being described.
    /// </summary>
    Type ServiceType { get; }

    /// <summary>
    /// Gets the optional key that distinguishes this service registration from others of the same type.
    /// </summary>
    /// <remarks>
    /// May be null for unkeyed service registrations.
    /// </remarks>
    object Key { get; }

    /// <summary>
    /// Creates a new service accessor capable of providing instances of the described service.
    /// </summary>
    /// <returns>
    /// An <see cref="IServiceAccessor"/> implementation specific to this service registration.
    /// </returns>
    IServiceAccessor CreateAccessor();
}