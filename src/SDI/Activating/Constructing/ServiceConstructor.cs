using System.Reflection;

namespace SDI.Activating.Constructing;

public sealed class ServiceConstructor(ConstructorInfo source) : ServiceConstructorBase<ConstructorInfo>(source)
{
    protected override object Invoke(ConstructorInfo source, object[] arguments) => source.Invoke(arguments);

    public static implicit operator ServiceConstructor(ConstructorInfo source) => new(source);
}
