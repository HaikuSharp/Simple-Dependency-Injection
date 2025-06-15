using System;

namespace SDI.Abstraction;

public interface IServiceLifeTimeTypeResolver<in TLifeTimeTypeSource>
{
    Type Resolve(TLifeTimeTypeSource source);
}
