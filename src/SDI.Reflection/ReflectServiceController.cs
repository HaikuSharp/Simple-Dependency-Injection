using SDI.Abstraction;
using SDI.Reflection.Resolving;

namespace SDI.Reflection;

public sealed class ReflectServiceController : ServiceController
{
    public static IServiceController Create()
    {
        ReflectServiceController controller = new();
        controller.SetupDefaultServices();
        return controller;
    }

    protected override void SetupDefaultServices()
    {
        base.SetupDefaultServices();
        RegisterWeakInstance<IServiceDependencyResolver>(DEFAULT_SERVICE_KEY, ServiceDependencyResolver.Default);
        RegisterWeakInstance<IServiceConstructorResolver>(DEFAULT_SERVICE_KEY, ServiceDefaultConstructorResolver.Default);
    }
}
