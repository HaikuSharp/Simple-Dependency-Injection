using SDI.Abstraction;
using SDI.Extensions;
using SDI.Resolving;

namespace SDI;

public sealed class DefaultServiceController : ServiceController
{
    public static IServiceController Create()
    {
        DefaultServiceController controller = new();
        controller.SetupDefaultServices();
        return controller;
    }

    protected override void SetupDefaultServices()
    {
        this.RegisterWeakInstance<IServiceController>(DEFAULT_SERVICE_KEY, this);
        this.RegisterWeakInstance<IServiceDependencyResolver>(DEFAULT_SERVICE_KEY, ServiceDependencyResolver.Default);
        this.RegisterWeakInstance<IServiceConstructorResolver>(DEFAULT_SERVICE_KEY, ServiceDefaultConstructorResolver.Default);
    }
}
