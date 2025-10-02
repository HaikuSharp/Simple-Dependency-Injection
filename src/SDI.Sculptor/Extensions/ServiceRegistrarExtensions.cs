using SDI.Abstraction;
using SDI.Sculptor.Scoping;

namespace SDI.Sculptor.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceRegistrar"/>.
/// </summary>
public static class ServiceRegistrarExtensions
{
    /// <summary>
    /// Begins a new registration session for the specified registrar.
    /// </summary>
    /// <typeparam name="TRegistrar">The type of the service registrar.</typeparam>
    /// <param name="registrar">The service registrar instance.</param>
    /// <returns>A new registration session.</returns>
    public static Registration<TRegistrar> RegistrationBegin<TRegistrar>(this TRegistrar registrar) where TRegistrar : class, IServiceRegistrar => new(registrar);
}