using SDI.Abstraction;
namespace SDI.Accessors;
public abstract class ServiceAccessorBase(ServiceId id) : IServiceAccessor {
 protected ServiceId Id {
  get {
   return id;
  }
 }
 public bool CanAccess(ServiceId otherId) {
  return otherId.Equals(id);
 }
 public abstract object Access(IServiceProvider provider);
}
