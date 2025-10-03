namespace SDI.Abstraction;

/// <summary>
/// Represents the service registrar.
/// </summary>
public interface IServiceRegistrar
{
    /// <summary>
    /// Determines whether a service with the specified identifier is currently registered.
    /// </summary>
    /// <param name="id">The service identifier to check.</param>
    /// <returns>
    /// <c>true</c> if the service is registered in the current controller scope or any parent scope;
    /// <c>false</c> otherwise.
    /// </returns>
    bool IsRegistered(ServiceId id);

    /// <summary>
    /// Registers a new service using the specified descriptor.
    /// </summary>
    /// <typeparam name="TDescriptor">The type of service descriptor implementing <see cref="IServiceDescriptor"/>.</typeparam>
    /// <param name="descriptor">The service descriptor containing registration details.</param>
    void RegisterService<TDescriptor>(TDescriptor descriptor) where TDescriptor : IServiceDescriptor;

    /// <summary>
    /// Removes a service registration from the current scope.
    /// </summary>
    /// <param name="id">The service identifier to unregister.</param>
    void UnregisterService(ServiceId id);
}
