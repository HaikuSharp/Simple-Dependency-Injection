using Microsoft.Testing.Platform.Services;
using SDI.Abstraction;
using SDI.Extensions;
using SDI.Reflection;
using SDI.Reflection.Extensions;
using SDI.Tests.Services;
using Sugar.Object.Extensions;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Tests;

[TestClass]
public sealed class ServiceProviderTestBase
{
    [TestMethod]
    public void DoAccessTest()
    {
        var controller = ReflectServiceController.Create();
        Register(controller);
        Access(controller.CreateDefaultScope());
    }

    private static void Register(IServiceController controller)
    {
        controller.RegisterTransientService<IServiceA, ServiceA>(ServiceController.DEFAULT_SERVICE_KEY);

        controller.RegisterLazySingletonService<IServiceB, ServiceB0>("0");
        controller.RegisterLazySingletonService<IServiceB, ServiceB1>("1");

        controller.RegisterLazySingletonService<IServiceC, ServiceC>(ServiceController.DEFAULT_SERVICE_KEY);

        controller.RegisterTransientService(typeof(IServiceG<,>), ServiceController.DEFAULT_SERVICE_KEY, typeof(ServiceG<>));
    }

    private static void Access(IServiceProvider provider)
    {
        var serviceAInstance0 = provider.GetRequiredService<IServiceA>();
        var serviceAInstance1 = provider.GetRequiredService<IServiceA>();

        Assert.AreNotEqual(serviceAInstance0, serviceAInstance1);

        provider.GetRequiredService<IServiceB>("0").Forget();
        provider.GetRequiredService<IServiceB>("1").Forget();

        var serviceCInstance0 = provider.GetRequiredService<IServiceC>();
        var serviceCInstance1 = provider.GetRequiredService<IServiceC>();

        Assert.AreEqual(serviceCInstance0, serviceCInstance1);
        Assert.AreEqual(2, serviceCInstance0.BCount);

        provider.GetRequiredService<IServiceG<IServiceA, IServiceB>>().Forget();
    }
}
