using SDI.Abstraction;
namespace SDI.Accessors;
public class InstanceServiceAccessor(ServiceId id, object instance) : ServiceAccessorBase(id) {
 public override object Access(IServiceProvider provider) {
  return instance;
 }
}
