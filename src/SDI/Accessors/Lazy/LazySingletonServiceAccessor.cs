using SDI.Abstraction;
namespace SDI.Accessors.Lazy;
public class LazySingletonServiceAccessor(ServiceId id, IServiceInstanceActivator activator, IServiceInstanceContanier contanier) : LazyServiceAccessorBase(id, activator) {
 public override object Access(IServiceProvider provider) {
  var id = this.Id;
  if(contanier.TryGetInstance(id, out var instace)) {
   return instace;
  }
  return contanier.Create(id, this.Activate(provider));
 }
}
