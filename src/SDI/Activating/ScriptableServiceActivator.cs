using SDI.Abstraction;
using System;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Activating;

/// <summary>
/// Service activator that uses a delegate function to create service instances.
/// </summary>
public sealed class ScriptableServiceActivator(ScriptableServiceActivator.Activator activator) : IServiceInstanceActivator
{
    private readonly Activator m_Activator = activator ?? throw new ArgumentNullException(nameof(activator));

    /// <summary>
    /// Delegate that defines the signature for service activation functions.
    /// </summary>
    /// <param name="requestedId">The service identifier being activated.</param>
    /// <param name="provider">The service provider for dependency resolution.</param>
    /// <returns>The activated service instance.</returns>
    public delegate object Activator(ServiceId requestedId, IServiceProvider provider);

    /// <inheritdoc/>
    public object Activate(ServiceId requestedId, IServiceProvider provider) => m_Activator(requestedId, provider);
}
