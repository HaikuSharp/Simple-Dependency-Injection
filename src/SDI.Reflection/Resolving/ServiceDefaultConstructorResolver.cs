using SDI.Reflection.Abstraction;
using SDI.Reflection.Activating.Constructing;
using SDI.Reflection.Exceptions;
using System;
using System.Linq;

namespace SDI.Reflection.Resolving;

/// <summary>
/// Resolves service constructors by selecting the one with the fewest parameters (greedy constructor resolution).
/// </summary>
public sealed class ServiceDefaultConstructorResolver : IServiceConstructorResolver
{
    /// <summary>
    /// The default singleton instance of the constructor resolver.
    /// </summary>
    public static ServiceDefaultConstructorResolver Default => field ??= new();

    /// <inheritdoc/>
    public IServiceConstructor Resolve(Type serviceImplementationType) => new ServiceConstructor(DefaultServiceConstructorNotFoundException.ThrowIfNull(serviceImplementationType.GetConstructors().OrderBy(c => c.GetParameters().Length).FirstOrDefault(), serviceImplementationType));
}