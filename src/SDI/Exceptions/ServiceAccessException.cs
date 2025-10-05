using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

/// <summary>
/// The exception that is thrown when a service instance cannot be accessed or is null.
/// </summary>
public sealed class ServiceAccessException(string message) : InvalidOperationException(message)
{
    /// <summary>
    /// Throws a <see cref="ServiceAccessException"/> if the specified service instance is null.
    /// </summary>
    /// <param name="id">The service identifier that was attempted to be accessed.</param>
    /// <param name="instance">The service instance to check for null.</param>
    /// <exception cref="ServiceAccessException">
    /// Thrown when <paramref name="instance"/> is null.
    /// </exception>
    public static object ThrowIfServiceInstanceIsNull(ServiceId id, object instance) => instance is not null ? instance : throw new ServiceAccessException($"Service {id} instance can't be null.");

    /// <summary>
    /// Throws a <see cref="ServiceAccessException"/> if the specified provider is the root provider.
    /// </summary>
    /// <param name="provider">The service scope provider to check.</param>
    /// <param name="id">The service identifier being accessed (used for error message).</param>
    /// <returns>The original provider if it is not the root provider.</returns>
    /// <exception cref="ServiceAccessException">Thrown when the provider is the root provider.</exception>
    public static IServiceScopedProvider ThrowIfIsRoot(IServiceScopedProvider provider, ServiceId id) => provider.IsRoot ? throw new ServiceAccessException($"Attempting to access a scoped service {id} from the root provider.") : provider;

    /// <summary>
    /// Performs a type cast and throws a <see cref="ServiceAccessException"/> if the cast is not valid.
    /// </summary>
    /// <typeparam name="TSource">The source type of the object to cast.</typeparam>
    /// <typeparam name="TCast">The target type to cast to.</typeparam>
    /// <param name="source">The object to cast.</param>
    /// <returns>The casted object if the cast is valid.</returns>
    /// <exception cref="ServiceAccessException">Thrown when the object cannot be cast to the requested type.</exception>
    public static TCast ThrowIfCastIsNotValid<TSource, TCast>(TSource source) => source is TCast cast ? cast : throw new ServiceAccessException($"The service type {source.GetType().FullName} does not match the requested one {typeof(TCast).FullName}.");
}