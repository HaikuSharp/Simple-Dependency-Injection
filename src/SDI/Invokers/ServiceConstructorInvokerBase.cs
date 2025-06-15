using SDI.Abstraction;
using System.Collections.Generic;
using System.Reflection;

namespace SDI.Invokers;

public class ServiceConstructorInvokerBase<TMethod>(TMethod method) : IServiceConstructorInvoker where TMethod : MethodBase
{
    public IEnumerable<ParameterInfo> Parameters => method.GetParameters();

    public object Invoke(object[] arguments) => Invoke(method, arguments);

    public virtual object Invoke(TMethod method, object[] arguments) => method?.Invoke(null, arguments);
}
