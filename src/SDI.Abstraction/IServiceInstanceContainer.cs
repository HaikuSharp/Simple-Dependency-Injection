using System;

namespace SDI.Abstraction;

public interface IServiceInstanceContainer : IDisposable
{
    ScopeId Id { get; }

    bool HasInstance(ServiceId id);

    object GetIsntance(ServiceId id);

    object Create(ServiceId id, object instance);

    void Dispose(ServiceId id);
}
