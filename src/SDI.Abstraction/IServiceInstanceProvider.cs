namespace SDI.Abstraction;
public interface IServiceInstanceProvider {
 bool HasInstance(ServiceId id);
 object GetInstance(ServiceId id);
 bool TryGetInstance(ServiceId id, out object instance);
}
