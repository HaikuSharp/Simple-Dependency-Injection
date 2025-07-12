namespace SDI.Abstraction;

public interface IServiceController
{
    bool IsRegistered(ServiceId id);

    void RegisterService<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor;

    void UnregisterService(ServiceId id);

    IServiceProvider CreateScope(ScopeId id);
}
