using System.Collections.Generic;
using System.Reflection;

namespace SDI.Reflection.Abstraction;

/// <summary>
/// Represents a constructor abstraction capable of creating service instances.
/// </summary>
public interface IServiceConstructor
{
    /// <summary>
    /// Gets the parameters required by this constructor.
    /// </summary>
    /// <value>
    /// A read-only list of <see cref="ParameterInfo"/> describing each parameter's type and metadata.
    /// </value>
    IReadOnlyList<ParameterInfo> Parameters { get; }

    /// <summary>
    /// Creates a new instance by invoking the constructor with the supplied arguments.
    /// </summary>
    /// <param name="arguments">The arguments to pass to the constructor.</param>
    /// <returns>The newly constructed service instance.</returns>
    object Invoke(object[] arguments);
}