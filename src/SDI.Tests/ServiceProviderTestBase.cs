using Microsoft.Testing.Platform.Services;
using SDI.Abstraction;
using SDI.Extensions;
using SDI.Reflection;
using SDI.Reflection.Extensions;
using SDI.Tests.Services;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Tests;

[TestClass]
public sealed class ServiceProviderTestBase
{
    [TestMethod]
    public void DoAccessTest()
    {
        var controller = ServiceControllerBase.Create<ReflectServiceController>();
        Register(controller);
        Access(controller);
    }

    private static void Register(IServiceController controller)
    {
        controller.RegisterTransientService<IServiceA, ServiceA>(ServiceControllerBase.DEFAULT_SERVICE_KEY);

        controller.RegisterLazySingletonService<IServiceB, ServiceB0>("0");
        controller.RegisterLazySingletonService<IServiceB, ServiceB1>("1");

        controller.RegisterLazySingletonService<IServiceC, ServiceC>(ServiceControllerBase.DEFAULT_SERVICE_KEY);

        controller.RegisterTransientService(typeof(IServiceG<,>), "0", typeof(ServiceG0<>));
        controller.RegisterTransientService(typeof(IServiceG<,>), "1", typeof(ServiceG1<>));
    }

    private static void Access(IServiceProvider provider)
    {
        var serviceAInstance0 = provider.GetRequiredService<IServiceA>();
        var serviceAInstance1 = provider.GetRequiredService<IServiceA>();

        Assert.AreNotEqual(serviceAInstance0, serviceAInstance1);

        _ = provider.GetRequiredService<IServiceB>("0");
        _ = provider.GetRequiredService<IServiceB>("1");

        var serviceCInstance0 = provider.GetRequiredService<IServiceC>();
        var serviceCInstance1 = provider.GetRequiredService<IServiceC>();

        Assert.AreEqual(serviceCInstance0, serviceCInstance1);
        Assert.AreEqual(2, serviceCInstance0.BCount);

        // When checking the registration, only ServiceType is taken into account,
        // so we need to specify unique keys,
        // otherwise Activator will not know which ImplementationType to use when substituting Generic arguments
        _ = provider.GetRequiredService<IServiceG<IServiceA, IServiceA>>("0");
        _ = provider.GetRequiredService<IServiceG<IServiceA, IServiceB>>("1");

        _ = provider.GetRequiredService("0");
    }
}
