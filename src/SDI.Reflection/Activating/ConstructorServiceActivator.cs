using SDI.Reflection.Abstraction;
using IServiceScopedProvider = SDI.Abstraction.IServiceScopedProvider;

namespace SDI.Reflection.Activating;

/// <summary>
/// Service activator that uses a pre-determined constructor for instance creation.
/// </summary>
public sealed class ConstructorServiceActivator(IServiceConstructor constructor) : ConstructorServiceActivatorBase
{
    /// <inheritdoc/>
    protected override IServiceConstructor GetConstructor(IServiceScopedProvider provider) => constructor;
}