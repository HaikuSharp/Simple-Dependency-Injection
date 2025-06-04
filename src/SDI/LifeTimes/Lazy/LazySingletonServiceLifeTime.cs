using SDI.Abstraction;
using SDI.Accessors.Lazy;
namespace SDI.LifeTimes.Lazy;
public class LazySingletonServiceLifeTime(IServiceActivatorResolver resolver) : LazyServiceLifeTimeBase(resolver) {
 protected override IServiceAccessor CreateAccessor(IServiceInstanceContanier contanier, IServiceDescriptor descriptor, IServiceInstanceActivator activator) {
  return new LazySingletonServiceAccessor(ServiceId.FromDescriptor(descriptor), activator, contanier);
 }
}
