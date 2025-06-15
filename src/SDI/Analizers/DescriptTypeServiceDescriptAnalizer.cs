using SDI.Abstraction;
using System;
using System.Collections.Generic;

namespace SDI.Analizers;

public class DescriptTypeServiceDescriptAnalizer(IServiceDescriptResolver<Type> resolver) : IServiceDescriptAnalizer<Type>
{
    public IEnumerable<IServiceDescriptor> Analize(Type source)
    {
        yield return resolver.Resolve(source);
    }
}