using SDI.Reflection.Abstraction;
using System.Reflection;

namespace SDI.Reflection.Activating.Constructing;

/// <summary>
/// Wrapper for a <see cref="ConstructorInfo"/> that implements <see cref="IServiceConstructor"/>.
/// </summary>
public sealed class ServiceConstructor(ConstructorInfo source) : ServiceConstructorBase<ConstructorInfo>(source)
{
    /// <inheritdoc/>
    protected override object Invoke(ConstructorInfo source, object[] arguments) => source.Invoke(arguments);
}
