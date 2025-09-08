using SDI.Reflection.Abstraction;

namespace SDI.Reflection.Resolving;

/// <summary>
/// Default implementation of <see cref="IServiceDependencyResolver"/> that creates
/// service dependencies without key-based resolution.
/// </summary>
public sealed class ServiceDependencyResolver : IServiceDependencyResolver
{
    /// <summary>
    /// The default singleton instance of the dependency resolver.
    /// </summary>
    public static ServiceDependencyResolver Default => field ??= new();

    /// <inheritdoc/>
    public IServiceDependency Resolve(ServiceDependencyInfo dependencyInfo) => new ServiceDependency(dependencyInfo.SourceType, null);
}
