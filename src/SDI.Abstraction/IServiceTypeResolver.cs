using System;

namespace SDI.Abstraction;

public interface IServiceTypeResolver<in TServiceTypeSource>
{
    Type Resolve(TServiceTypeSource source);
}