using System;

namespace SDI.Abstraction;

public interface IServiceInstanceContainer : IServiceInstanceProvider, IDisposable
{
    object Create(ServiceId id, object instance);

    void Dispose(ServiceId id);

    void DisposeAll();
}
