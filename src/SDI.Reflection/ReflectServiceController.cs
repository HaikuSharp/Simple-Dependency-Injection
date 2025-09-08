using SDI.Reflection.Abstraction;
using SDI.Reflection.Resolving;

namespace SDI.Reflection;

/// <summary>
/// A service controller with reflection-based dependency resolution capabilities.
/// </summary>
public class ReflectServiceController : ServiceController
{
    /// <inheritdoc/>
    protected override void SetupDefaultServices()
    {
        base.SetupDefaultServices();
        RegisterWeakInstance<IServiceDependencyResolver>(DEFAULT_SERVICE_KEY, ServiceDependencyResolver.Default);
        RegisterWeakInstance<IServiceConstructorResolver>(DEFAULT_SERVICE_KEY, ServiceDefaultConstructorResolver.Default);
    }
}
