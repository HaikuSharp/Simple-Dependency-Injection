using System;
using System.Reflection;

namespace SDI.Reflection.Exceptions;

/// <summary>
/// The exception that is thrown when a default constructor cannot be found for a service implementation type.
/// </summary>
public sealed class InvalidServiceConstructor(string message) : Exception(message)
{
    /// <summary>
    /// Throws a <see cref="InvalidServiceConstructor"/> if the specified constructor is null.
    /// </summary>
    /// <typeparam name="TConstructor">The constructor type (either <see cref="ConstructorInfo"/> or <see cref="MethodBase"/>).</typeparam>
    /// <param name="constructor">The constructor to validate.</param>
    /// <param name="type">The service implementation type that was searched.</param>
    /// <returns>The input constructor if it is not null.</returns>
    /// <exception cref="InvalidServiceConstructor">
    /// Thrown when <paramref name="constructor"/> is null.
    /// </exception>
    public static TConstructor ThrowIfNull<TConstructor>(TConstructor constructor, Type type) where TConstructor : MethodBase => constructor is not null ? constructor : throw new InvalidServiceConstructor($"Service implementation {type.FullName} constructor not found.");
}