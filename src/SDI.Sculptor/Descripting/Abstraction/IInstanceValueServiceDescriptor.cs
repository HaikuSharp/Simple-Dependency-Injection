namespace SDI.Sculptor.Descripting.Abstraction;

/// <summary>
/// Represents a service descriptor that uses a pre-existing instance.
/// </summary>
/// <typeparam name="TSelf">The self-referencing generic type parameter.</typeparam>
public interface IInstanceValueServiceDescriptor<TSelf> : IValueServiceDescriptor<TSelf> where TSelf : IInstanceValueServiceDescriptor<TSelf>
{
    /// <summary>
    /// Creates a new descriptor with the specified instance.
    /// </summary>
    /// <param name="instance">The service instance.</param>
    /// <returns>A new descriptor instance with the specified service instance.</returns>
    TSelf WithInstance(object instance);
}