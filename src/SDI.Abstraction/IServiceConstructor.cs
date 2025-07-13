using System.Collections.Generic;
using System.Reflection;

namespace SDI.Abstraction;

public interface IServiceConstructor
{
    IReadOnlyList<ParameterInfo> Parameters { get; }

    object Invoke(object[] arguments);
}