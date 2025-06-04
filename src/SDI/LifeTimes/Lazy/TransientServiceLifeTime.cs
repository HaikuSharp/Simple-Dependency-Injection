using SDI.Abstraction;
using SDI.Accessors.Lazy;
namespace SDI.LifeTimes.Lazy;
public class TransientServiceLifeTime(IServiceActivatorResolver resolver) : LazyServiceLifeTimeBase(resolver) {
 protected override IServiceAccessor CreateAccessor(IServiceInstanceContanier contanier, IServiceDescriptor descriptor, IServiceInstanceActivator activator) {
  return new TransientServiceAccessor(ServiceId.FromDescriptor(descriptor), activator);
 }
}