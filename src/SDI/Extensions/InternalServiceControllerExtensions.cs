using SDI.Abstraction;
using SDI.Accessing;

namespace SDI.Extensions;

internal static class InternalServiceControllerExtensions
{
    internal static void RegisterInstance<TInstance>(this ServiceController controller, object id, TInstance instance) => controller.RegisterAccessor(new SingletonServiceAccessor(ServiceId.From<TInstance>(id), instance));

    internal static void RegisterWeakInstance<TInstance>(this ServiceController controller, object id, TInstance instance) => controller.RegisterAccessor(new WeakSingletonServiceAccessor(ServiceId.From<TInstance>(id), instance));

    internal static void UnregisterInstance<TInstance>(this ServiceController controller, object id) => controller.UnregisterService(ServiceId.From<TInstance>(id));
}
