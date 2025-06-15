using SDI.Extensions;
using SDI.Test.Types;

namespace SDI.Test;

[TestClass]
public sealed class ControllerTest
{
    [TestMethod]
    public void DoServiceKeyTest()
    {
        ServiceController controller = GetController();
        IFirstService firstService0 = controller.GetService<IFirstService>("0");
        firstService0.PrintHello();
        IFirstService firstService1 = controller.GetService<IFirstService>("1");
        firstService1.PrintHello();
    }

    [TestMethod]
    public void DoEnumerableDependencyTest()
    {
        ISecondService secondService = GetController().GetService<ISecondService>();
        secondService.PrintHello();
        secondService.First.PrintHello();
    }

    private static ServiceController GetController() => ServiceController.Create().RegisterServices(typeof(ControllerTest).Assembly);
}
