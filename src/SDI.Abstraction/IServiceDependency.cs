using System;

namespace SDI.Abstraction;

public interface IServiceDependency
{
    Type ServiceType { get; }

    object Key { get; }

    object GetDependency(IServiceProvider provider);
}
