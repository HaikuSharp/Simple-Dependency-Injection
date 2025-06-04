using System;
using System.Collections;
using System.Collections.Generic;
namespace SDI.Abstraction;
public interface IServiceProvider : IDisposable {
 bool IsImplemented(ServiceId id);
 IEnumerable GetServices(ServiceId id);
 object GetService(ServiceId id);
 IServiceProvider RegisterServices(IEnumerable<IServiceDescriptor> descriptors);
 IServiceProvider RegisterService(IServiceDescriptor descriptor);
 IServiceProvider UnregisterService(ServiceId id);
}
