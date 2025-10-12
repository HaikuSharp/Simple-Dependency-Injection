using SDI.Reflection.Abstraction;

namespace SDI.Reflection.Activating;

/// <summary>
/// Service activator that uses a pre-determined constructor for instance creation.
/// </summary>
public sealed class ConstructorServiceActivator(IServiceConstructor constructor, IServiceDependencyResolver dependencyResolver) : ConstructorServiceActivatorBase(dependencyResolver)
{
    /// <inheritdoc/>
    protected override IServiceConstructor GetConstructor(SDI.Abstraction.IServiceProvider provider) => constructor;
}