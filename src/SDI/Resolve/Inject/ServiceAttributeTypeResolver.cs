using SDI.Abstraction;
using SDI.Attribute;
using System;
using System.Linq;
using System.Reflection;

namespace SDI.Resolve.Inject;

public class ServiceAttributeTypeResolver<TServiceTypeSource> : IServiceTypeResolver<TServiceTypeSource> where TServiceTypeSource : ICustomAttributeProvider
{
    public Type Resolve(TServiceTypeSource source) => source.GetCustomAttributes(typeof(TypeInjectAttribute), true).Cast<TypeInjectAttribute>().FirstOrDefault()?.ServiceType;
}
