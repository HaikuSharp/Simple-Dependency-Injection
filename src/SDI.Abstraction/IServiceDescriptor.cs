using System;
namespace SDI.Abstraction;
public interface IServiceDescriptor {
 Type ServiceType { get; }
 Type ImplementationType { get; }
 Type LifeTimeType { get; }
 object Key { get; }
}
