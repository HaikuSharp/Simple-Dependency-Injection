using System;
using System.Collections.Generic;
using System.Reflection;

namespace SDI.Abstraction;

public interface IServiceConstructor
{
    Type DeclaringType { get; }

    IReadOnlyList<ParameterInfo> Parameters { get; }

    object Invoke(object[] arguments);
}