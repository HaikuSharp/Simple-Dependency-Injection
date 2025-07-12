using System;

namespace SDI.Abstraction;

public interface IServiceDescriptor
{
    Type ServiceType { get; }

    object Key { get; }

    Type ImplementationType { get; }

    IServiceAccessor CreateAccessor();
}