using SDI.Abstraction;

namespace SDI;

public sealed class DefaultServiceController : ServiceController
{
    public static IServiceController Create()
    {
        DefaultServiceController controller = new();
        controller.SetupDefaultServices();
        return controller;
    }
}
