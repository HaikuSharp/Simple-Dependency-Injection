using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

/// <summary>
/// The exception that is thrown when a service instance cannot be accessed or is null.
/// </summary>
public class ServiceAccessException(ServiceId id) : NullReferenceException($"Service [id: {id}] instance can't be null.")
{
    /// <summary>
    /// Throws a <see cref="ServiceAccessException"/> if the specified service instance is null.
    /// </summary>
    /// <param name="id">The service identifier that was attempted to be accessed.</param>
    /// <param name="instance">The service instance to check for null.</param>
    /// <exception cref="ServiceAccessException">
    /// Thrown when <paramref name="instance"/> is null.
    /// </exception>
    public static void ThrowIfServiceInstanceIsNull(ServiceId id, object instance)
    {
        if(instance is not null) return;
        throw new ServiceAccessException(id);
    }
}