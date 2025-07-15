using SDI.Reflection.Abstraction;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Reflection.Activating;

/// <summary>
/// Service activator that uses a pre-determined constructor for instance creation.
/// </summary>
public class ConstructorServiceActivator(IServiceConstructor constructor) : ConstructorServiceActivatorBase
{
    /// <inheritdoc/>
    protected override IServiceConstructor GetConstructor(IServiceProvider provider) => constructor;
}