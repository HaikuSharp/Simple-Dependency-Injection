using SDI.Abstraction;
using System;

namespace SDI.Exceptions;

/// <summary>
/// The exception that occurs when attempting to use a type on an id that has no type.
/// </summary>
public class InvalidServiceIdException(string message) : Exception(message)
{
    /// <summary>
    /// Throws a <see cref="InvalidServiceIdException"/> if the specified id has no type.
    /// </summary>
    /// <param name="id">The specified id.</param>
    /// <exception cref="InvalidServiceIdException">
    /// Thrown when <paramref name="id"/> has no type.
    /// </exception>
    public static void ThrowIfTypeIsNull(ServiceId id)
    {
        if(id.HasType) return;
        throw new InvalidServiceIdException($"The specified id ({id}) has no type.");
    }
}
