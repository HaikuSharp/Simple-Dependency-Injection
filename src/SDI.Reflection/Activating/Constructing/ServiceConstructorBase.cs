using SDI.Reflection.Abstraction;
using System.Collections.Generic;
using System.Reflection;

namespace SDI.Reflection.Activating.Constructing;

/// <summary>
/// Base class for service constructors that provides common functionality for method invocation.
/// </summary>
/// <typeparam name="TMethod">The method base type (either <see cref="ConstructorInfo"/> or <see cref="MethodBase"/>).</typeparam>
public abstract class ServiceConstructorBase<TMethod>(TMethod source) : IServiceConstructor where TMethod : MethodBase
{
    /// <inheritdoc/>
    public IReadOnlyList<ParameterInfo> Parameters => source.GetParameters();

    /// <inheritdoc/>
    public object Invoke(object[] arguments) => Invoke(source, arguments);

    /// <summary>
    /// When implemented in derived classes, performs the actual constructor/method invocation.
    /// </summary>
    /// <param name="source">The constructor or factory method to invoke.</param>
    /// <param name="arguments">The arguments to pass.</param>
    /// <returns>The newly created service instance.</returns>
    protected abstract object Invoke(TMethod source, object[] arguments);
}