using SDI.Abstraction;
using System;

namespace SDI.Sculptor.Descripting.Abstraction;

/// <summary>
/// Represents a service descriptor that can create service identifiers.
/// </summary>
/// <typeparam name="TSelf">The self-referencing generic type parameter.</typeparam>
public interface IValueServiceDescriptor<TSelf> : IServiceDescriptor where TSelf : IValueServiceDescriptor<TSelf>
{
    /// <summary>
    /// Creates a new descriptor with the specified type and key.
    /// </summary>
    /// <param name="type">The service type.</param>
    /// <param name="key">The service key.</param>
    /// <returns>A new descriptor instance with the specified type and key.</returns>
    TSelf WithId(Type type, object key);
}