namespace SDI.Reflection.Abstraction;

/// <summary>
/// Represents a component capable of resolving service dependencies into executable dependency handlers.
/// </summary>
public interface IServiceDependencyResolver
{
    /// <summary>
    /// Resolves a dependency descriptor into a concrete dependency handler.
    /// </summary>
    /// <param name="dependencyInfo">The dependency metadata to resolve.</param>
    /// <returns>
    /// An <see cref="IServiceDependency"/> implementation that can materialize the dependency when needed.
    /// </returns>
    IServiceDependency Resolve(ServiceDependencyInfo dependencyInfo);
}