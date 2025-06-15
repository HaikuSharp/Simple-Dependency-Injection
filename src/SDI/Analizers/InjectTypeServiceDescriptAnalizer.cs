using SDI.Abstraction;
using SDI.Attribute;
using System;
using System.Collections.Generic;
namespace SDI.Analizers;
public class InjectTypeServiceDescriptAnalizer(IServiceTypeResolver<Type> typeResolver, IServiceKeyResolver<Type> keyResolver, IServiceLifeTimeTypeResolver<Type> lifeTimeTypeResolver) : IServiceDescriptAnalizer<Type>
{
    public IEnumerable<IServiceDescriptor> Analize(Type source)
    {
        if(!source.IsDefined(typeof(InjectAttribute), true)) yield break;
        yield return new ServiceDescriptor(typeResolver.Resolve(source) ?? source, source, lifeTimeTypeResolver.Resolve(source) ?? source, keyResolver.Resolve(source));
    }
}
