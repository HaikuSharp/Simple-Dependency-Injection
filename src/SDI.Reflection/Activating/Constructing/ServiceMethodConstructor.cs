using SDI.Reflection.Abstraction;
using System.Reflection;

namespace SDI.Reflection.Activating.Constructing;

/// /// <summary>
/// Wrapper for a static factory <see cref="MethodInfo"/> that implements <see cref="IServiceConstructor"/>.
/// </summary>
public sealed class ServiceMethodConstructor(MethodInfo source) : ServiceConstructorBase<MethodInfo>(source)
{
    /// <inheritdoc/>
    protected override object Invoke(MethodInfo source, object[] arguments) => source.Invoke(null, arguments);
}
