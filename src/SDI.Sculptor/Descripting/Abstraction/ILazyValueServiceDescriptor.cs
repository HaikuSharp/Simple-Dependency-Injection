using SDI.Abstraction;

namespace SDI.Sculptor.Descripting.Abstraction;

/// <summary>
/// Represents a service descriptor that uses lazy initialization with an activator.
/// </summary>
/// <typeparam name="TSelf">The self-referencing generic type parameter.</typeparam>
public interface ILazyValueServiceDescriptor<TSelf> : IValueServiceDescriptor<TSelf> where TSelf : ILazyValueServiceDescriptor<TSelf>
{
    /// <summary>
    /// Creates a new descriptor with the specified activator.
    /// </summary>
    /// <param name="activator">The service instance activator.</param>
    /// <returns>A new descriptor instance with the specified activator.</returns>
    TSelf WithActivator(IServiceInstanceActivator activator);
}