using System;
using System.Collections;

namespace SDI.Abstraction;

public interface IServiceProvider : System.IServiceProvider, IDisposable
{
    ScopeId Id { get; }

    IServiceController Controller { get; }

    IServiceInstanceContainer Container { get; }

    bool IsImplemented(ServiceId id);

    IEnumerable GetServices(ServiceId id);

    object GetService(ServiceId id);
}
