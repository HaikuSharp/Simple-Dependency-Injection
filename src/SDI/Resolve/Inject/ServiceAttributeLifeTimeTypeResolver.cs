using SDI.Abstraction;
using SDI.Attribute;
using System;
using System.Linq;
using System.Reflection;

namespace SDI.Resolve.Inject;

public class ServiceAttributeLifeTimeTypeResolver<TServiceLifeTimeTypeSource> : IServiceLifeTimeTypeResolver<TServiceLifeTimeTypeSource> where TServiceLifeTimeTypeSource : ICustomAttributeProvider
{
    public Type Resolve(TServiceLifeTimeTypeSource source) => source.GetCustomAttributes(typeof(LifeTimeInjectAttribute), true).Cast<LifeTimeInjectAttribute>().FirstOrDefault()?.LifeTimeType;
}
