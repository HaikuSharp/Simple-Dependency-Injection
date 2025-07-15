using System;

namespace SDI.Reflection.Abstraction;

/// <summary>
/// Represents a component that resolves and provides constructors for service instantiation.
/// </summary>
public interface IServiceConstructorResolver
{
    /// <summary>
    /// Resolves the appropriate constructor for creating instances of the specified service implementation type.
    /// </summary>
    /// <param name="serviceImplementationType">The concrete type to be instantiated.</param>
    /// <returns>
    /// An <see cref="IServiceConstructor"/> instance representing the resolved constructor.
    /// </returns>
    IServiceConstructor Resolve(Type serviceImplementationType);
}