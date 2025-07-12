using System.Reflection;

namespace SDI.Activating.Constructing;

public sealed class ServiceMethodConstructor(MethodInfo source) : ServiceConstructorBase<MethodInfo>(source)
{
    protected override object Invoke(MethodInfo source, object[] arguments) => source.Invoke(null, arguments);

    public static implicit operator ServiceMethodConstructor(MethodInfo source) => new(source);
}
