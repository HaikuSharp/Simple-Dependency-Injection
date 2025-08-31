using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

/// <summary>
/// The exception that is thrown when attempting to access a scope that has not been created or is invalid.
/// </summary>
public class ScopeNotCreatedExeption(ScopeId id) : Exception($"Scope with [id: {id}] not created.")
{
    /// <summary>
    /// Throws a <see cref="ScopeNotCreatedExeption"/> if the specified container is null.
    /// </summary>
    /// <param name="container">The service instance container to validate.</param>
    /// <param name="id">The scope id.</param>
    /// <returns>
    /// The input <paramref name="container"/> if it is not null.
    /// </returns>
    /// <exception cref="ScopeNotCreatedExeption">
    /// Thrown when <paramref name="container"/> is null.
    /// </exception>
    public static IServiceInstanceContainer ThrowIfNull(IServiceInstanceContainer container, ScopeId id) => container is not null ? container : throw new ScopeNotCreatedExeption(id);
}