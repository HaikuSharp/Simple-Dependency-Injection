using SDI.Abstraction;
using SDI.Extensions;
using SDI.Tests.Services;
using IServiceProvider = SDI.Abstraction.IServiceProvider;

namespace SDI.Tests;

[TestClass]
public sealed class ServiceProviderTestBase
{
    [TestMethod]
    public void DoAccessTest()
    {
        var controller = ServiceController.Create();
        Register(controller);
        Access(controller.CreateScope(ScopeId.Default));
    }

    private static void Register(IServiceController controller)
    {
        controller.RegisterTransientService<IServiceA, ServiceA>("default");

        controller.RegisterLazySingletonService<IServiceB, ServiceB0>("0");
        controller.RegisterLazySingletonService<IServiceB, ServiceB1>("1");

        controller.RegisterLazySingletonService<IServiceC, ServiceC>("default");
    }

    private static void Access(IServiceProvider provider)
    {
        var serviceAInstance0 = provider.GetService<IServiceA>();
        var serviceAInstance1 = provider.GetService<IServiceA>();

        Assert.IsNotNull(serviceAInstance0);
        Assert.IsNotNull(serviceAInstance1);
        Assert.AreNotEqual(serviceAInstance0, serviceAInstance1);

        Assert.IsNotNull(provider.GetService<IServiceB>("0"));
        Assert.IsNotNull(provider.GetService<IServiceB>("1"));

        var serviceCInstance0 = provider.GetService<IServiceC>();
        var serviceCInstance1 = provider.GetService<IServiceC>();

        Assert.IsNotNull(serviceCInstance0);
        Assert.IsNotNull(serviceCInstance1);
        Assert.AreEqual(serviceCInstance0, serviceCInstance1);
        Assert.AreEqual(2, serviceCInstance0.BCount);
    }
}
