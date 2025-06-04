using System;
namespace SDI.Abstraction;
public interface IServiceInstanceContanier : IServiceInstanceProvider, IDisposable {
 object Create(ServiceId id, object instance);
 void Dispose(ServiceId id);
 void DisposeAll();
}
