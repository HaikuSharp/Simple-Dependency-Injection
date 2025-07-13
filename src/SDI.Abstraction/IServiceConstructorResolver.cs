using System;

namespace SDI.Abstraction;

public interface IServiceConstructorResolver
{
    IServiceConstructor Resolve(Type serviceImplementationType);
}