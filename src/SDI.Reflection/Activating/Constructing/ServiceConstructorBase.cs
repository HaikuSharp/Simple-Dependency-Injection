using SDI.Abstraction;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SDI.Reflection.Activating.Constructing;

public abstract class ServiceConstructorBase<TMethod>(TMethod source) : IServiceConstructor where TMethod : MethodBase
{
    public Type DeclaringType => source.DeclaringType;

    public IReadOnlyList<ParameterInfo> Parameters => source.GetParameters();

    public object Invoke(object[] arguments) => Invoke(source, arguments);

    protected abstract object Invoke(TMethod source, object[] arguments);
}
