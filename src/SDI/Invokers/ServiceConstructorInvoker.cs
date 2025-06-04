using System.Reflection;
namespace SDI.Invokers;
public class ServiceConstructorInvoker(ConstructorInfo constructor) : ServiceConstructorInvokerBase<ConstructorInfo>(constructor) {
 public override object Invoke(ConstructorInfo constructor, object[] arguments) {
  return constructor?.Invoke(arguments);
 }
}