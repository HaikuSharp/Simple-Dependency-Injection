using System;

namespace SDI.Abstraction;

public interface IServiceDescriptor
{
    Type ServiceType { get; }

    object Key { get; }

    IServiceAccessor CreateAccessor();
}