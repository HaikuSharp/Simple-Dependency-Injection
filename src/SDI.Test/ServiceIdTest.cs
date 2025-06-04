using SDI.Abstraction;
using SDI.Test.Types;
namespace SDI.Test;
[TestClass]
public sealed class ServiceIdTest {
 [TestMethod]
 public void TestEquals() {
  var firstAnyId = ServiceId.From<IFirstService>();
  var first0Id = ServiceId.From<IFirstService>("0");
  Assert.IsTrue(firstAnyId.Equals(first0Id));
  // Assert.IsTrue(first0Id.Equals(firstAnyId)); // throw Exception
 }
}