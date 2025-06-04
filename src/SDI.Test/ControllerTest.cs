using SDI.Extensions;
using SDI.Test.Types;
using Sugar.Object.Extensions;
namespace SDI.Test;
[TestClass]
public sealed class ControllerTest {
 [TestMethod]
 public void DoServiceKeyTest() {
  var controller = GetController();
  var firstService0 = controller.GetService<IFirstService>("0");
  firstService0.PrintHello();
  var firstService1 = controller.GetService<IFirstService>("1");
  firstService1.PrintHello();
 }
 [TestMethod]
 public void DoEnumerableDependencyTest() {
  var secondService = GetController().GetService<ISecondService>();
  secondService.PrintHello();
  secondService.First.PrintHello();
 }
 private static ServiceController GetController() {
  return ServiceController.Create().RegisterServices(typeof(ControllerTest).Assembly);
 }
}
